using DoodleJump.Core.Signals;
using UnityEngine;
using Zenject;

namespace DoodleJump.Core.Services
{
    public class ScoreService : IScoreService
    {
        private const string RecordKey = "HighScore";
        
        private int _currentScore;
        private readonly SignalBus _signalBus;

        [Inject]
        public ScoreService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int GetRecord()
        {
            return PlayerPrefs.GetInt(RecordKey, 0);
        }

        public void SaveRecord(int score)
        {
            PlayerPrefs.SetInt(RecordKey, score);
            PlayerPrefs.Save();
        }

        public void AddScore()
        {
            _currentScore++;
            
            _signalBus.Fire(new ScoreChangedSignal(_currentScore));
        }

        public void ResetScore()
        {
            _currentScore = 0;
           
            _signalBus.Fire(new ScoreChangedSignal(_currentScore));
        }

        public bool TryUpdateRecord(int score)
        {
            int currentRecord = GetRecord();
            
            if (score > currentRecord)
            {
                SaveRecord(score);
                return true;
            }

            return false;
        }
    }
}
