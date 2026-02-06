namespace DoodleJump.Core.Services
{
    public interface ISceneLoadingService
    {
        void LoadMenu();
        void LoadGameplay();
        void ReloadGameplay();
        void Quit();
    }
}
