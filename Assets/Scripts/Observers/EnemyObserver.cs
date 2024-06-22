using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : ObjectsObserver
{
    private PoolManager _enemyPool;
    private RegisterListenersComponent _registerListeners = new RegisterListenersComponent();
    public EnemyObserver(PoolManager pool) : base(pool)
    {
        this._enemyPool = pool;
    }

    public override void Subscribe(GameObject enemyObject)
    {
        _registerListeners.RegisterListeners(enemyObject);
        enemyObject.GetComponent<HitPointsComponent>().hpEmpty += this.HandleDisableEvent;
    }

    protected override void HandleDisableEvent(GameObject enemyObject)
    {
        _registerListeners.UnregisterListeners(enemyObject);
        _enemyPool.EnqueueItem(enemyObject);
        enemyObject.GetComponent<HitPointsComponent>().hpEmpty -= this.HandleDisableEvent;
    }

   
}
