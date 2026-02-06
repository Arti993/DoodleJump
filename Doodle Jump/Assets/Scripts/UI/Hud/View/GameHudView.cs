using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using DoodleJump.UI.Hud.ViewModel;

namespace DoodleJump.UI.Hud.View
{
    public class GameHudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitToMenuButton;

        private GameHudViewModel _viewModel;

        [Inject]
        public void Construct(GameHudViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ScoreChanged += OnScoreChanged;
            _viewModel.GameOver += OnGameOver;

            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(false);

            if (_restartButton != null)
                _restartButton.onClick.AddListener(_viewModel.OnRestartClicked);

            if (_exitToMenuButton != null)
                _exitToMenuButton.onClick.AddListener(_viewModel.OnExitToMenuClicked);

            OnScoreChanged(_viewModel.Score);
        }

        private void OnDestroy()
        {
            if (_viewModel != null)
            {
                _viewModel.ScoreChanged -= OnScoreChanged;
                _viewModel.GameOver -= OnGameOver;

                if (_restartButton != null)
                    _restartButton.onClick.RemoveListener(_viewModel.OnRestartClicked);

                if (_exitToMenuButton != null)
                    _exitToMenuButton.onClick.RemoveListener(_viewModel.OnExitToMenuClicked);
            }
        }

        private void OnScoreChanged(int score)
        {
            if (_scoreText != null)
                _scoreText.text = score.ToString();
        }

        private void OnGameOver()
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);
        }
    }
}
