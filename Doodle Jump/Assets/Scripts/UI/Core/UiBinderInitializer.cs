using System;
using Zenject;

namespace DoodleJump.UI.Core
{
    public class UiBinderInitializer : IInitializable, IDisposable
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

        public void Dispose()
        {
            _binder.Unbind();
        }
    }
}
