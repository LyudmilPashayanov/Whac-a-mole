using System;
using System.Collections.Generic;
using Miniclip.Entities;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.Json;

namespace Miniclip.Playfab
{
    public class PlayfabManager
    {
        private readonly string _titleId = "C9050";
        private string _playerPlayfabID = "";
        
        private Action _loadingFinished;
        private Action _errorOccured; // TODO: make it an event?

        public GameData GameData;
        public PlayerData PlayerAttemptData = new PlayerData();
        public PlayerOptionsData PlayerOptionsData = new PlayerOptionsData();
        public WorldsData WorldsAttemptData = new WorldsData();

        public void Init(Action loadingFinished, Action errorOccured)
        {
            _loadingFinished = loadingFinished;
            _errorOccured = errorOccured;
            PlayFabSettings.TitleId = _titleId;
            Login();
        }

        /// <summary>
        /// Connects to the Playfab service
        /// </summary>
        private void Login()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDevice = SystemInfo.deviceModel,
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnPlayFabError);
#endif
#if UNITY_EDITOR
            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayFabError);
#endif
        }

        private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
        {
            _playerPlayfabID = result.PlayFabId;
            GetTitleData();
        }

        /// <summary>
        /// Gets the title data stored on the server and sets it for use in the game
        /// </summary>
        private void GetTitleData()
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
                result =>
                {
                    if (result.Data != null)
                    {
                        // Get data from title data and save it to memory
                        if (result.Data.ContainsKey("gameplay_rules"))
                        {
                            Dictionary<string,int> tempData = PlayFabSimpleJson.DeserializeObject<Dictionary<string,int>>(result.Data["gameplay_rules"]);
                            GameData = new GameData(tempData["Timer"], tempData["PointsPerHit"], tempData["ComboX2"],
                                tempData["ComboX3"], tempData["ConsecutiveHitsRequired"]);
                        }
                        GetPlayerData();
                    }
                },
                OnPlayFabError);
        }

        /// <summary>
        /// Gets the data of the currently logged player and sets it for use in the game
        /// </summary>
        private void GetPlayerData()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = _playerPlayfabID
            }, result =>
            {
                if (result.Data.TryGetValue("attempts", out var attempts))
                {
                    PlayerAttemptData = PlayFabSimpleJson.DeserializeObject<PlayerData>(attempts.Value);
                }
                if (result.Data.TryGetValue("options", out var options))
                {
                    PlayerOptionsData = PlayFabSimpleJson.DeserializeObject<PlayerOptionsData>(options.Value);
                }
                DoneLoading();
                }, OnPlayFabError);
        }

        /// <summary>
        /// Marks the game as loaded and starts it
        /// </summary>
        private void DoneLoading()
        {
            _loadingFinished?.Invoke(); 
        }

        /// <summary>
        /// Saves the local attempt data of the player.
        /// </summary>
        /// <param name="newAttemptData"></param>
        public void SavePlayerAttempts(PlayerData newAttemptData)
        { 
            UpdateUserDataRequest request = new UpdateUserDataRequest();
            request.Data = new Dictionary<string, string>() {
                {"attempts", PlayFabSimpleJson.SerializeObject(newAttemptData)}};
            
            PlayFabClientAPI.UpdateUserData(request,
                result=>{},
                OnPlayFabError);
        }
        /// <summary>
        /// Saves the Player options. In this case if the player has seen the tutorial or not.
        /// </summary>
        /// <param name="optionsData"></param>
        public void SavePlayerOptions(PlayerOptionsData optionsData)
        { 
            UpdateUserDataRequest request = new UpdateUserDataRequest();
            request.Data = new Dictionary<string, string>() {
                {"options", PlayFabSimpleJson.SerializeObject(optionsData)}};
            
            PlayFabClientAPI.UpdateUserData(request,
                result => {},
                OnPlayFabError);
        }
        /// <summary>
        /// Adds your highest results to the leaderboard
        /// </summary>
        /// <param name="recordAttempt"></param>
        public void UpdateLeaderboard(AttemptData recordAttempt)
        {
            UpdateDisplayName(recordAttempt.Name);
            var playerStat = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "Highscores",
                    Value = recordAttempt.Score
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest{
                Statistics = playerStat
            }, result => {
            }, error => {
#if UNITY_EDITOR
                Debug.LogError("Leaderboard update failed: " + error.ErrorMessage);
#endif
            });
        }
        /// <summary>
        /// Updates the display name of the player
        /// </summary>
        /// <param name="newDisplayName"></param>
        private void UpdateDisplayName(string newDisplayName)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = newDisplayName
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, (result)=>
            { }, (error) =>
            {
#if UNITY_EDITOR
                Debug.LogError("Error updating display name: " + error.ErrorMessage);
#endif
            });
        }

        /// <summary>
        /// Gets the world wide leaderboard
        /// </summary>
        /// <param name="worldsAttemptDataRetrieved"></param>
        public void GetLeaderboard(Action<WorldsData> worldsAttemptDataRetrieved)
        {
            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest{
                StatisticName = "Highscores",
                StartPosition = 0,
                MaxResultsCount = 10
            }, result => {
                List<AttemptData> worldData = new List<AttemptData>();
                foreach (var player in result.Leaderboard)
                {
                    AttemptData data = new AttemptData();
                    data.Name = player.DisplayName;
                    data.Score = player.StatValue;
                    worldData.Add(data);
                }
                WorldsAttemptData.worldWideAttempts = worldData;
                worldsAttemptDataRetrieved?.Invoke(WorldsAttemptData);

            }, error => {
#if UNITY_EDITOR
                Debug.LogError("Leaderboard retrieval failed: " + error.ErrorMessage);
#endif
            });

        }
        
        /// <summary>
        /// Failed call or to the server, stops the app and prompts a restart. 
        /// </summary>
        /// <param name="error"></param>
        private void OnPlayFabError(PlayFabError error) //TODO: Better handling could be done here for example: retrying, saving locally, etc.
        {
#if UNITY_EDITOR
            Debug.Log("ON PLAYFAB ERROR:  " + error.ErrorMessage);
#endif
            _errorOccured?.Invoke();
        }
    }
}