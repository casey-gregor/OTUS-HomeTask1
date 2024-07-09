using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ListenersStorage
    {
        public List<IGameListener> gameListeners = new();

        public ListenersStorage()
        {
            IGameListener.RegisterEvent += RegisterEventHandler;
        }

        private void RegisterEventHandler(IGameListener gameListener)
        {
            gameListeners.Add(gameListener);
        }
    }

}
