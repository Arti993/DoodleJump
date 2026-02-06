using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DoodleJump.UI.Menu.ViewModel;

namespace DoodleJump.UI.Menu.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private MenuViewModel _viewModel;

        [Inject]
        public void Construct(MenuViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_playButton != null)
                _playButton.onClick.AddListener(_viewModel.OnPlayClicked);

            if (_exitButton != null)
                _exitButton.onClick.AddListener(_viewModel.OnExitClicked);
        }

        private void OnDestroy()
        {
            if (_viewModel != null)
            {
                if (_playButton != null)
                    _playButton.onClick.RemoveListener(_viewModel.OnPlayClicked);

                if (_exitButton != null)
                    _exitButton.onClick.RemoveListener(_viewModel.OnExitClicked);
            }
        }
    }
}
