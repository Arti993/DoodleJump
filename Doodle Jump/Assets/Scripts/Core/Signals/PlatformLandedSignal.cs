namespace DoodleJump.Core.Signals
{
    public class PlatformLandedSignal
    {
        public int PlatformInstanceID { get; }

        public PlatformLandedSignal(int platformInstanceID)
        {
            PlatformInstanceID = platformInstanceID;
        }
    }
}
