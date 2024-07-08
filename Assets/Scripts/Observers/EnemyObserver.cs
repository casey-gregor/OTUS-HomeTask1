using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : ObjectsObserver
{
    private Pool enemyPool;
    private EnemyHitPointsComponent hitPointsComponent;

    private RegisterListenersComponent registerListeners = new RegisterListenersComponent();
    public EnemyObserver(Pool pool, EnemyHitPointsComponent hitPointsComponent) : base(pool)
    {
        this.enemyPool = pool;
        this.hitPointsComponent = hitPointsComponent;

        hitPointsComponent.hpEmptyEvent += this.HandleDisableEvent;
    }

    protected override void HandleDisableEvent(GameObject enemyObject)
    {
        
        registerListeners.UnregisterListeners(enemyObject);
        enemyPool.EnqueueItem(enemyObject);
    }

   
}
