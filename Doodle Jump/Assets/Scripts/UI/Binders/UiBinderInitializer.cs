using System;
using Zenject;

namespace DoodleJump.UI.Binders
{
    public class UiBinderInitializer : IInitializable, IDisposable
    {
        private readonly IBinder _binder;

        [Inject]
        public UiBinderInitializer(IBinder binder)
        {
            _binder = binder;
        }

        public void Dispose()
        {
            _binder.Unbind();
        }

        public void Initialize()
        {
            _binder.Bind();
        }
    }
}