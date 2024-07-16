using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class ListenersStorage
    {
        private List<IGameListener> gameListeners = new();
        public List<IGameListener> Listeners => gameListeners;
        public void AddToListeners(IGameListener listener)
        {
            this.gameListeners.Add(listener);
        }

    }

}
