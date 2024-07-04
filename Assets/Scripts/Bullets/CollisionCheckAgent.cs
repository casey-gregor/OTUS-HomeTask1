using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckAgent
{
    public event Action<GameObject> OnCollisionEntered;
    private BulletConfig bulletConfig;

    public CollisionCheckAgent(BulletConfig bulletConfig)
    {
        this.bulletConfig = bulletConfig;
        //Debug.Log("CollisionCheckAgent created");
    }

    public void CheckCollision(Collision2D collider, GameObject bullet)
    {
        //Debug.Log($"checking collision of {collider.gameObject.name} and {bullet}");
        if (!collider.gameObject.TryGetComponent(out TeamComponent team))
            return;
        if (bulletConfig.isPlayer == team.IsPlayer)
            return;

        collider.gameObject.GetComponent<HitPointsComponentMono>().TakeDamage(bulletConfig.damage);
        Debug.Log($"{bullet.name} has collided");
        this.OnCollisionEntered?.Invoke(bullet);
    }

}
