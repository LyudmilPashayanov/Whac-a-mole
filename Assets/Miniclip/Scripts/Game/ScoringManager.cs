using System;
using Miniclip.Entities;

namespace Miniclip.Game
{
    public struct ScoreData
    {
        public int Combo;
        public int Hits;
        public int HitsInARow;
    }
    
    public class ScoringManager
    {
        private readonly GameData _gameData;
        private ScoreData _scoreData;
        private event Action<ScoreData> OnScoreUpdated;
        
        public ScoringManager(GameData gameData, Action<ScoreData> scoreUpdated)
        {
            _gameData = gameData;
            _scoreData = new ScoreData();
            OnScoreUpdated += scoreUpdated;
        }
        
       
        public void CalculateScoring(MoleType moleType)
        {
            if (moleType == MoleType.Bomb)
            {
                ResetComboPoints();
            }
            else
            {
                ReceivePoints();
                IncreaseComboPoints();
            }

            OnScoreUpdated?.Invoke(_scoreData);
        }

        private void ReceivePoints()
        {
            _scoreData.HitsInARow++;
            for (int i = 0; i < _scoreData.Combo; i++)
            {
                _scoreData.Hits++;
            }

            if (_scoreData.HitsInARow == _gameData.ConsecutiveHitsRequired)
            {
                _scoreData.HitsInARow = 0;
                _scoreData.Hits++;
            }
        }

        private void IncreaseComboPoints()
        {
            if (_scoreData.HitsInARow == _gameData.ComboX2)
            {
                _scoreData.Combo = 2;
            }
            else if(_scoreData.HitsInARow == _gameData.ComboX3)
            {
                _scoreData.Combo = 3;
            }
        }

        private void ResetComboPoints()
        {
            _scoreData.HitsInARow = 0;
            _scoreData.Combo = 1;
        }
    }
}
