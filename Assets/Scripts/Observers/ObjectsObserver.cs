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
        protected abstract void HandleDisableEvent(GameObject obj);
    }
}
