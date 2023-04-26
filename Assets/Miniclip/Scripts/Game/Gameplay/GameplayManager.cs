using System;
using System.Collections.Generic;
using Random = System.Random;

namespace Miniclip.Game.Gameplay
{
    /// <summary>
    /// During gameplay the difficulty changes, this enum represents the different stages of difficulty.
    /// </summary>
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    
    /// <summary>
    /// C# class decoupled from the Unity engine and responsible for the logic of the game
    /// </summary>
    public class GameplayManager
    {
        #region Variables

        private readonly MoleFactory _factory;
        private readonly Random _random;
        
        // The following list could be retrieved from a backend eventually.
        private readonly MoleType[] _easyPool = new[] { MoleType.Normal, MoleType.Fortified };
        private readonly MoleType[] _mediumPool = new[] { MoleType.Normal, MoleType.Fortified, MoleType.Bomb};
        private readonly MoleType[] _hardPool = new[] { MoleType.Fortified, MoleType.Bomb };
        
        private Difficulty _currentDifficulty;
        private List<int> _availablePositions = new List<int>() {0,1,2,3,4,5,6};
        private int _spawnedMoles;
        private event Action OnMoleSpawned;

        #endregion

        #region Functionality

           public GameplayManager(MoleFactory moleFactory)
        {
            _factory = moleFactory;
            _random = new Random();
            OnMoleSpawned += ModifyDifficulty;
        }
        
        /// <summary>
        /// Gets a Mole from the Mole Factory 
        /// </summary>
        /// <param name="moleType">The type of Mole, which you want to retrieve (changes during gameplay)</param>
        /// <returns></returns>
        public MoleController SpawnMole(MoleType moleType)
        {
            MoleController mole = _factory.GetMole(moleType);
            mole.SetShowSpeed(GetShowingSpeed());
            _spawnedMoles++;
            OnMoleSpawned?.Invoke();
            return mole;
        }
        
        /// <summary>
        /// Checks which spawn position is free in order to spawn a mole there.
        /// </summary>
        /// <returns></returns>
        public int GetRandomPosition()
        {
            int randomIndex = _random.Next(_availablePositions.Count);
            int returnValue = _availablePositions[randomIndex];
            _availablePositions.Remove(returnValue);
            return returnValue;
        }

        public void FreeSpawnPosition(int index)
        {
            _availablePositions.Add(index);
        }

        #region In-game Difficulty Logic

         private void ModifyDifficulty()
        {
            switch (_spawnedMoles)
            {
                case 25:
                    _currentDifficulty = Difficulty.Medium;
                    break;
                case 50:
                    _currentDifficulty = Difficulty.Hard;
                    break;
            }
        }
        
        public MoleType GetRandomMoleType()
        {
            int randomIndex;
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    randomIndex = _random.Next(_easyPool.Length);
                    return _easyPool[randomIndex];
                
                case Difficulty.Medium:
                    randomIndex = _random.Next(_mediumPool.Length);
                    return _mediumPool[randomIndex];
                case Difficulty.Hard:
                    randomIndex = _random.Next(_hardPool.Length);
                    return _hardPool[randomIndex];
                default:
                    return MoleType.Normal; 
            }
        }
        
        private float GetShowingSpeed()
        {
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    return 0.7f;
                case Difficulty.Medium:
                    return 0.6f;
                case Difficulty.Hard:
                    return 0.5f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public float GetTimeBetweenMoles()
        {
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    return 0.8f;
                case Difficulty.Medium:
                    return 0.6f;
                case Difficulty.Hard:
                    return 0.4f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public float GetMoleAliveTime()
        {
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    return 0.7f;
                case Difficulty.Medium:
                    return 0.6f;
                case Difficulty.Hard:
                    return 0.4f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
        
        public void ResetManager()
        {
            _currentDifficulty = Difficulty.Easy;
            _spawnedMoles = 0;
        }

        #endregion
     
    }
}
