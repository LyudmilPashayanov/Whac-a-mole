using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = System.Random;

namespace Miniclip.Game.Gameplay
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    
    public class GameplayManager
    {
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
        public GameplayManager(MoleFactory moleFactory)
        {
            _factory = moleFactory;
            _random = new Random();
            OnMoleSpawned += ModifyDifficulty;
        }
        
        public MoleController SpawnMole(MoleType moleType)
        {
            MoleController mole = _factory.GetMole(moleType);
            _spawnedMoles++;
            OnMoleSpawned?.Invoke();
            return mole;
        }

        private void ModifyDifficulty()
        {
            switch (_spawnedMoles)
            {
                case 10:
                    _currentDifficulty = Difficulty.Medium;
                    break;
                case 20:
                    _currentDifficulty = Difficulty.Hard;
                    break;
            }
        }
        
        public int GetRandomPosition()
        {
            int randomIndex = _random.Next(_availablePositions.Count-1);
            int returnValue = _availablePositions[randomIndex];
            _availablePositions.Remove(returnValue);
            return returnValue;
        }

        public void FreeSpawnPosition(int index)
        {
            _availablePositions.Add(index);
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
        
        public float GetTimeBetweenMoles()
        {
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    return 1f;
                case Difficulty.Medium:
                    return 0.8f;
                case Difficulty.Hard:
                    return 0.5f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public float GetMoleAliveTime()
        {
            switch (_currentDifficulty)
            {
                case Difficulty.Easy:
                    return 0.9f;
                case Difficulty.Medium:
                    return 0.6f;
                case Difficulty.Hard:
                    return 0.5f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
