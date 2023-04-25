using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Miniclip.Game.Gameplay
{
    public class GameplayManager
    {
        private readonly MoleFactory _factory;
        private readonly Random _random;

        private List<int> _availablePositions = new List<int>() {0,1,2,3,4,5,6};

        public GameplayManager(MoleFactory moleFactory)
        {
            _factory = moleFactory;
            _random = new Random();
        }
        
        public MoleController GetRandomMole()
        {
            int randomIndex = _random.Next(Enum.GetNames(typeof(MoleType)).Length);
            MoleType randomMole = (MoleType)randomIndex;
            return _factory.GetMole(randomMole);
        }

        public int GetRandomPosition()
        {
            int randomIndex = _random.Next(_availablePositions.Count);
            _availablePositions.RemoveAt(randomIndex);
            return randomIndex;
        }

        public void FreeSpawnPosition(int index)
        {
            _availablePositions.Add(index);
        }
    }
}
