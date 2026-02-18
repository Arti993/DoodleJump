using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.UI.Views
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