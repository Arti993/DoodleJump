using System;
using Zenject;
using DoodleJump.Core.Services;
using DoodleJump.Core.Signals;

namespace DoodleJump.UI.Hud.ViewModel
{
    public class GameHudViewModel : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly ISceneLoadingService _sceneLoadingService;
        private int _score;

        public int Score => _score;

        public event Action<int> ScoreChanged;
        public event Action GameOver;

        [Inject]
        public GameHudViewModel(SignalBus signalBus, ISceneLoadingService sceneLoadingService)
        {
            _signalBus = signalBus;
            _sceneLoadingService = sceneLoadingService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<ScoreChangedSignal>(OnScoreChanged);
            _signalBus.TryUnsubscribe<GameOverSignal>(OnGameOver);
        }

        public void Reset()
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            _score = signal.Score;
            ScoreChanged?.Invoke(_score);
        }

        private void OnGameOver()
        {
            GameOver?.Invoke();
        }

        public void OnRestartClicked()
        {
            _sceneLoadingService.ReloadGameplay();
        }

        public void OnExitToMenuClicked()
        {
            _sceneLoadingService.LoadMenu();
        }
    }
}
