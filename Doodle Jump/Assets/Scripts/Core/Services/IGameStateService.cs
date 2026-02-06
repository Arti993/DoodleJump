namespace DoodleJump.Core.Services
{
    public interface IGameStateService
    {
        GameState CurrentState { get; }
        void SetState(GameState state);
    }
}
