using System;
using Miniclip.Entities;

namespace Miniclip.Game.Gameplay
{
    public class GameplayManager
    {
        private readonly MoleFactory _factory;
        private readonly Random _random;

        private GameData _gameData;

        public GameplayManager(GameData gameData, MoleFactory moleFactory)
        {
            _factory = moleFactory;
            _gameData = gameData;
            _random = new Random();
        }
        
        public MoleController GetRandomMole()
        {
            int randomIndex = _random.Next(Enum.GetNames(typeof(MoleType)).Length);
            MoleType randomMole = (MoleType)randomIndex;
            return _factory.GetMole(randomMole);
        }
    }
}
