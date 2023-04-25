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
#if !UNITY_ANDROID
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
                if (result.Data.ContainsKey("attempts"))
                {
                    PlayerAttemptData = PlayFabSimpleJson.DeserializeObject<PlayerData>(result.Data["attempts"].Value);
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

        public void SavePlayerAttempts(PlayerData newAttemptData)
        { 
            UpdateUserDataRequest request = new UpdateUserDataRequest();
            request.Data = new Dictionary<string, string>() {
                {"attempts", PlayFabSimpleJson.SerializeObject(newAttemptData)}};
            
            PlayFabClientAPI.UpdateUserData(request,
                result => Debug.Log("Successfully updated user data"),
                OnPlayFabError);
        }

        /// <summary>
        /// Failed call or to the server, stops the app and prompts a restart. 
        /// </summary>
        /// <param name="error"></param>
        private void OnPlayFabError(PlayFabError error) //TODO: Better handling could be done here for example: retrying, saving locally, etc.
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log("ON PLAYFAB ERROR:  " + error.ErrorMessage);
#endif
            _errorOccured?.Invoke();
            // Set pop up message to the player that something is wrong. For example: UIManager.SetNoConnectionError();
        }
    }
}