using DoodleJump.UI.ViewModels;
using DoodleJump.UI.Views;
using Zenject;

namespace DoodleJump.UI.Binders
{
    public class GameHudBinder : IBinder
    {
        private readonly GameHudView _view;
        private readonly GameHudViewModel _viewModel;

        [Inject]
        public GameHudBinder(GameHudView view, GameHudViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Bind()
        {
            _viewModel.ScoreChanged += OnScoreChanged;
            _viewModel.GameOver += OnGameOver;

            if (_view.RestartButton != null)
                _view.RestartButton.onClick.AddListener(_viewModel.OnRestartClicked);

            if (_view.ExitToMenuButton != null)
                _view.ExitToMenuButton.onClick.AddListener(_viewModel.OnExitToMenuClicked);

            OnScoreChanged(_viewModel.Score);
        }

        public void Unbind()
        {
            _viewModel.ScoreChanged -= OnScoreChanged;
            _viewModel.GameOver -= OnGameOver;

            if (_view.RestartButton != null)
                _view.RestartButton.onClick.RemoveListener(_viewModel.OnRestartClicked);

            if (_view.ExitToMenuButton != null)
                _view.ExitToMenuButton.onClick.RemoveListener(_viewModel.OnExitToMenuClicked);
        }

        private void OnScoreChanged(int score)
        {
            _view.UpdateScore(score);
        }

        private void OnGameOver(int score, int record)
        {
            _view.ShowGameOver(score, record);
        }
    }
}