namespace DoodleJump.Core.Signals
{
    public class InputDirectionSignal
    {
        public float Direction { get; }

        public InputDirectionSignal(float direction)
        {
            Direction = direction;
        }
    }
}
