using UnityEngine;

namespace ShootEmUp
{
    public abstract class ObjectsObserver
    {
        private Pool pool;
        protected ObjectsObserver(Pool pool)
        {
            this.pool = pool;
        }
        public abstract void Subscribe(GameObject obj);
        protected abstract void HandleDisableEvent(GameObject obj);
    }
}
