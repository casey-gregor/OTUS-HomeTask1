using ShootEmUp;
using System;
using UnityEngine;


namespace ShootEmUp
{
    public class BulletObserver : ObjectsObserver
    {
        private Pool bulletPool;
        private RegisterListenersComponent registerListeners = new RegisterListenersComponent();
        public BulletObserver(Pool bulletPool) : base(bulletPool)
        {
            this.bulletPool = bulletPool;
        }

        public override void Subscribe(GameObject bulletObject)
        {
            registerListeners.RegisterListeners(bulletObject);
            bulletObject.GetComponent<CollisionCheckComponent>().OnCollisionEntered += this.HandleDisableEvent;
            bulletObject.GetComponent<LevelBoundsCheckComponent>().OnOutOfBounds += this.HandleDisableEvent;
        }

        protected override void HandleDisableEvent(GameObject bulletObject)
        {
            registerListeners.UnregisterListeners(bulletObject);
            bulletObject.GetComponent<CollisionCheckComponent>().OnCollisionEntered -= this.HandleDisableEvent;
            bulletObject.GetComponent<LevelBoundsCheckComponent>().OnOutOfBounds -= this.HandleDisableEvent;
            bulletPool.EnqueueItem(bulletObject);
        }
    }

}
