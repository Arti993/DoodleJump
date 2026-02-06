using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DoodleJump.Core.Services
{
    public class SceneLoadingService : ISceneLoadingService
    {
        [Inject]
        public SceneLoadingService() { }

        public void LoadMenu()
        {
            SceneManager.LoadScene(DoodleJump.Utils.Constants.Scenes.Menu);
        }

        public void LoadGameplay()
        {
            SceneManager.LoadScene(DoodleJump.Utils.Constants.Scenes.Gameplay);
        }

        public void ReloadGameplay()
        {
            SceneManager.LoadScene(DoodleJump.Utils.Constants.Scenes.Gameplay);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            System.Type editorApplication = System.Type.GetType("UnityEditor.EditorApplication, UnityEditor");
            editorApplication?.GetProperty("isPlaying")?.SetValue(null, false);
#else
            Application.Quit();
#endif
        }
    }
}
