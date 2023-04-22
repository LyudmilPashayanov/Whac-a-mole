using System;
using Miniclip.Entities;

namespace Miniclip.Game.Gameplay
{
    public class GameplayManager
    {
        private MoleFactory _factory;
        private GameData _gameData;
        private Random _random;

        public GameplayManager(GameData gameData, MoleFactory moleFactory)
        {
            _factory = moleFactory;
            _gameData = gameData;
            _random = new Random(); 
        }

        /*
        public Mole GetRandomMole()
        {
            int randomIndex = _random.Next(3);
            MoleType randomMole = (MoleType)randomIndex;
            
           // return _factory.CreateMole(randomMole);
        }*/
    }
}
