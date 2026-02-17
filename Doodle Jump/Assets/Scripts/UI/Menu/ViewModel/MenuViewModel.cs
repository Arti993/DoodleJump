using Zenject;
using DoodleJump.UI.Core;
using DoodleJump.UI.Menu.Model;

namespace DoodleJump.UI.Menu.ViewModel
{
    public class MenuViewModel : IViewModel
    {
        private readonly MenuModel _model;

        [Inject]
        public MenuViewModel(MenuModel model)
        {
            _model = model;
        }

        public void OnPlayClicked()
        {
            _model.Play();
        }

        public void OnExitClicked()
        {
            _model.Exit();
        }
    }
}
