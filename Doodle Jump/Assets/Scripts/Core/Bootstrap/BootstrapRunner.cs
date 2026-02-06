using DoodleJump.Core.Services;
using Zenject;

namespace DoodleJump.Core.Bootstrap
{
    public class BootstrapRunner : IInitializable
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        [Inject]
        public BootstrapRunner(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void Initialize()
        {
            _sceneLoadingService.LoadMenu();
        }
    }
}
