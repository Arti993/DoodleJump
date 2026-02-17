using System;
using Zenject;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;

namespace DoodleJump.UI.Hud.Model
{
    public class GameHudModel : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IScoreService _scoreService;
        private readonly ISceneLoadingService _sceneLoadingService;

        private int _score;
        private int _record;
        private bool _isGameOver;

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

        public int Score => _score;
        public int Record => _record;
        public bool IsGameOver => _isGameOver;

        public event Action<int> ScoreChanged;
        public event Action<int, int> GameOver;

        public void Initialize()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            
            _score = _scoreService.GetCurrentScore();
            _record = _scoreService.GetRecord();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.TryUnsubscribe<GameOverSignal>(OnGameOver);
        }

        public void Restart()
        {
            _scoreService.ResetScore();
            _score = _scoreService.GetCurrentScore();
            _isGameOver = false;
            
            ScoreChanged?.Invoke(_score);
            _sceneLoadingService.ReloadGameplay();
        }

        public void ExitToMenu()
        {
            _sceneLoadingService.LoadMenu();
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            _score = signal.Score;
            ScoreChanged?.Invoke(_score);
        }

        private void OnGameOver()
        {
            _scoreService.TryUpdateRecord(_score);
            _record = _scoreService.GetRecord();
            _isGameOver = true;
            
            GameOver?.Invoke(_score, _record);
        }
    }
}
