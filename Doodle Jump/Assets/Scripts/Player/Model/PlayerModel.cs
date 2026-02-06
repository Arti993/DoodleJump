using Zenject;
using DoodleJump.Core;
using DoodleJump.Core.Signals;

namespace DoodleJump.Player.Model
{
    public class PlayerModel
    {
        private int _score;
        private bool _isAlive = true;
        private readonly SignalBus _signalBus;

        public int Score => _score;
        public bool IsAlive => _isAlive;

        [Inject]
        public PlayerModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void AddScore(int points)
        {
            _score += points;
            _signalBus.Fire(new ScoreChangedSignal(_score));
        }

        public void Die()
        {
            if (!_isAlive)
                return;

            _isAlive = false;
            _signalBus.Fire<PlayerDiedSignal>();
        }

        public void Reset()
        {
            _score = 0;
            _isAlive = true;
            _signalBus.Fire(new ScoreChangedSignal(0));
        }
    }
}
