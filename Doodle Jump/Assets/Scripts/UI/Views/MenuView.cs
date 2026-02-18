using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.UI.Views
{
    public class MenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        public Button PlayButton => _playButton;
        public Button ExitButton => _exitButton;
    }
}