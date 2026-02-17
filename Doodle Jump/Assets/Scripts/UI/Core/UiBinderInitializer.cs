using Zenject;

namespace DoodleJump.UI.Core
{
    public class UiBinderInitializer : IInitializable
    {
        private readonly IBinder _binder;

        [Inject]
        public UiBinderInitializer(IBinder binder)
        {
            _binder = binder;
        }

        public void Initialize()
        {
            _binder.Bind();
        }
    }
}
