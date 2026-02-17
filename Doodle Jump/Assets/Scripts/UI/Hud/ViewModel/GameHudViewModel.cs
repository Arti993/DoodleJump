using System;
using Zenject;
using DoodleJump.UI.Core;
using DoodleJump.UI.Hud.Model;

namespace DoodleJump.UI.Hud.ViewModel
{
    public class GameHudViewModel : IViewModel
    {
        private readonly GameHudModel _model;

        [Inject]
        public GameHudViewModel(GameHudModel model)
        {
            _model = model;
        }

        public int Score => _model.Score;
        public int Record => _model.Record;
        public bool IsGameOver => _model.IsGameOver;

        public event Action<int> ScoreChanged
        {
            add => _model.ScoreChanged += value;
            remove => _model.ScoreChanged -= value;
        }

        public event Action<int, int> GameOver
        {
            add => _model.GameOver += value;
            remove => _model.GameOver -= value;
        }

        public void OnRestartClicked()
        {
            _model.Restart();
        }

        public void OnExitToMenuClicked()
        {
            _model.ExitToMenu();
        }
    }
}
