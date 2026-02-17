using Zenject;
using DoodleJump.Core.Services;

namespace DoodleJump.UI.Menu.Model
{
    public class MenuModel
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        [Inject]
        public MenuModel(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void Play()
        {
            _sceneLoadingService.LoadGameplay();
        }

        public void Exit()
        {
            _sceneLoadingService.Quit();
        }
    }
}
