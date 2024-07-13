using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ListenersStorage : IDisposable
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

        public void Dispose()
        {
            IGameListener.RegisterEvent -= RegisterEventHandler;
        }
    }

}
