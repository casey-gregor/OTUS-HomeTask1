using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver : ObjectsObserver
{
    private IFactory _enemyFactory;
    public EnemyObserver(IFactory factory) : base(factory)
    {
        this._enemyFactory = factory;
    }

    public override void SubscribeToObject(GameObject obj)
    {
        obj.GetComponent<HitPointsComponent>().hpEmpty += this.OnHPempty;
    }

    void OnHPempty(GameObject enemy)
    {
        _enemyFactory.RemoveObject(enemy);
        enemy.GetComponent<HitPointsComponent>().hpEmpty -= this.OnHPempty;
    }
}
