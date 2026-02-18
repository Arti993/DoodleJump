namespace DoodleJump.Core.Services
{
    public interface IScoreService
    {
        int GetCurrentScore();
        int GetRecord();
        void SaveRecord(int score);
        void AddScore();
        void ResetScore();
        bool TryUpdateRecord(int score);
    }
}