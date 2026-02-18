using DoodleJump.UI.ViewModels;
using DoodleJump.UI.Views;
using Zenject;

namespace DoodleJump.UI.Binders
{
    public class MenuBinder : IBinder
    {
        private readonly MenuView _view;
        private readonly MenuViewModel _viewModel;

        [Inject]
        public MenuBinder(MenuView view, MenuViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Bind()
        {
            if (_view.PlayButton != null)
                _view.PlayButton.onClick.AddListener(_viewModel.OnPlayClicked);

            if (_view.ExitButton != null)
                _view.ExitButton.onClick.AddListener(_viewModel.OnExitClicked);
        }

        public void Unbind()
        {
            if (_view.PlayButton != null)
                _view.PlayButton.onClick.RemoveListener(_viewModel.OnPlayClicked);

            if (_view.ExitButton != null)
                _view.ExitButton.onClick.RemoveListener(_viewModel.OnExitClicked);
        }
    }
}