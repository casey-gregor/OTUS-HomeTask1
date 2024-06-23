using UnityEngine;

namespace ShootEmUp
{
    public abstract class ObjectsObserver
    {
        private PoolManager poolManager;
        protected ObjectsObserver(PoolManager poolManager)
        {
            this.poolManager = poolManager;
        }
        public abstract void Subscribe(GameObject obj);
        protected abstract void HandleDisableEvent(GameObject obj);
    }
}
