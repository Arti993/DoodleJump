using Zenject;
using DoodleJump.Core.Services;

namespace DoodleJump.UI.Menu.ViewModel
{
    public class MenuViewModel
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        [Inject]
        public MenuViewModel(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void OnPlayClicked()
        {
            _sceneLoadingService.LoadGameplay();
        }

        public void OnExitClicked()
        {
            _sceneLoadingService.Quit();
        }
    }
}
