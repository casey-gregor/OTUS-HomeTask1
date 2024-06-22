using ShootEmUp;
using System;
using UnityEngine;

public class BulletObserver : ObjectsObserver
{
    private PoolManager _bulletPool;
    private RegisterListenersComponent _registerListeners = new RegisterListenersComponent();
    public BulletObserver(PoolManager bulletPool) : base(bulletPool)
    {
        this._bulletPool = bulletPool;
    }

    public override void Subscribe(GameObject bulletObject)
    {
        _registerListeners.RegisterListeners(bulletObject);
        bulletObject.GetComponent<CollisionCheckComponent>().OnCollisionEntered += this.HandleDisableEvent;
        bulletObject.GetComponent<LevelBoundsCheckComponent>().OnOutOfBounds += this.HandleDisableEvent;
    }

    protected override void HandleDisableEvent(GameObject bulletObject)
    {
        _registerListeners.UnregisterListeners(bulletObject);
        bulletObject.GetComponent<CollisionCheckComponent>().OnCollisionEntered -= this.HandleDisableEvent;
        bulletObject.GetComponent<LevelBoundsCheckComponent>().OnOutOfBounds -= this.HandleDisableEvent;
        _bulletPool.EnqueueItem(bulletObject);
    }
}
