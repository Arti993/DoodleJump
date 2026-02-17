using DoodleJump.Data;
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
            SceneManager.LoadScene(SceneNames.MenuScene.ToString());
        }

        public void LoadGameplay()
        {
            SceneManager.LoadScene(SceneNames.GameplayScene.ToString());
        }

        public void ReloadGameplay()
        {
            SceneManager.LoadScene(SceneNames.GameplayScene.ToString());
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
