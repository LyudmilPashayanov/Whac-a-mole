using System;
using Miniclip.Entities;
using Miniclip.Entities.Moles;

namespace Miniclip.Game.Gameplay
{
    public class GameplayManager
    {
        private MoleFactory _factory;
        private GameData _gameData;
        private Random _random; 
        
        public void Init(GameData gameData)
        {
            _factory = new MoleFactory();
            _gameData = gameData;
            _random = new Random();
        }

        public Mole GetRandomMole()
        {
            int randomIndex = _random.Next(3);
            MoleType randomMole = (MoleType)randomIndex;
            
            return _factory.CreateMole(randomMole);
        }
    }
}
