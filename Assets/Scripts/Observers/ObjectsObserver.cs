using UnityEngine;

namespace ShootEmUp
{
    public abstract class ObjectsObserver
    {
        private PoolManager _poolManager;
        protected ObjectsObserver(PoolManager poolManager)
        {
            this._poolManager = poolManager;
        }
        public abstract void Subscribe(GameObject obj);
        protected abstract void HandleDisableEvent(GameObject obj);
    }
}
