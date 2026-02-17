using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoodleJump.UI.Core;

namespace DoodleJump.UI.Hud.View
{
    public class GameHudView : MonoBehaviour, IView
    {
        private const string Points = "Points: ";
        private const string Record = "Record: ";

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMP_Text _gameOverScoreText;
        [SerializeField] private TMP_Text _gameOverRecordText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitToMenuButton;

        public TMP_Text ScoreText => _scoreText;
        public GameObject GameOverPanel => _gameOverPanel;
        public TMP_Text GameOverScoreText => _gameOverScoreText;
        public TMP_Text GameOverRecordText => _gameOverRecordText;
        public Button RestartButton => _restartButton;
        public Button ExitToMenuButton => _exitToMenuButton;

        private void Awake()
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            if (_scoreText != null)
                _scoreText.text = score.ToString();
        }

        public void ShowGameOver(int score, int record)
        {
            if (_gameOverPanel != null)
                _gameOverPanel.SetActive(true);

            if (_gameOverScoreText != null)
                _gameOverScoreText.text = $"{Points}{score}";

            if (_gameOverRecordText != null)
                _gameOverRecordText.text = $"{Record}{record}";
        }
    }
}
