namespace DoodleJump.Core.Signals
{
    public class ScoreChangedSignal
    {
        public int Score { get; }

        public ScoreChangedSignal(int score)
        {
            Score = score;
        }
    }
}
