using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : ObjectsObserver
{
    private PoolManager enemyPool;
    private RegisterListenersComponent registerListeners = new RegisterListenersComponent();
    public EnemyObserver(PoolManager pool) : base(pool)
    {
        this.enemyPool = pool;
    }

    public override void Subscribe(GameObject enemyObject)
    {
        registerListeners.RegisterListeners(enemyObject);
        enemyObject.GetComponent<HitPointsComponent>().hpEmpty += this.HandleDisableEvent;
    }

    protected override void HandleDisableEvent(GameObject enemyObject)
    {
        registerListeners.UnregisterListeners(enemyObject);
        enemyPool.EnqueueItem(enemyObject);
        enemyObject.GetComponent<HitPointsComponent>().hpEmpty -= this.HandleDisableEvent;
    }

   
}
