using System;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;
using Zenject;

namespace DoodleJump.UI.Models
{
    public class GameHudModel : IInitializable, IDisposable
    {
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly IScoreService _scoreService;
        private readonly SignalBus _signalBus;

        [Inject]
        public GameHudModel(
            SignalBus signalBus,
            IScoreService scoreService,
            ISceneLoadingService sceneLoadingService)
        {
            _signalBus = signalBus;
            _scoreService = scoreService;
            _sceneLoadingService = sceneLoadingService;
        }

        public int Score { get; private set; }

        public int Record { get; private set; }
        
        public event Action<int> ScoreChanged;
        public event Action<int, int> GameOver;

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.TryUnsubscribe<GameOverSignal>(OnGameOver);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);

            Score = _scoreService.GetCurrentScore();
            Record = _scoreService.GetRecord();
        }
        
        public void Restart()
        {
            _scoreService.ResetScore();
            
            _sceneLoadingService.ReloadGameplay();
        }

        public void ExitToMenu()
        {
            _scoreService.ResetScore();
            
            _sceneLoadingService.LoadMenu();
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            Score = signal.Score;
            
            ScoreChanged?.Invoke(Score);
        }

        private void OnGameOver()
        {
            _scoreService.TryUpdateRecord(Score);
            
            Record = _scoreService.GetRecord();

            GameOver?.Invoke(Score, Record);
        }
    }
}