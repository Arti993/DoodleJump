using DoodleJump.UI.Models;
using Zenject;

namespace DoodleJump.UI.ViewModels
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