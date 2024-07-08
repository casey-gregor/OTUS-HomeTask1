using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionCheckComponent
{
    public event Action<GameObject> CollisionEnterEvent;
    public event Action<GameObject> DealDamageEvent;

    private int playerLayer;
    private int enemyLayer;
    private int playerBulletLayer;
    private int enemyBulletLayer;



    public BulletCollisionCheckComponent()
    {
        //Debug.Log("CollisionCheckAgent created");
        playerLayer = LayerMask.NameToLayer("Character");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerBulletLayer = LayerMask.NameToLayer("PlayerBullet");
        enemyBulletLayer = LayerMask.NameToLayer("EnemyBullet");


    }

    public void CheckCollision(Collision2D collider, GameObject bullet)
    {
        if ((bullet.gameObject.layer == enemyBulletLayer && collider.gameObject.layer == playerLayer) ||
            (bullet.gameObject.layer == playerBulletLayer && collider.gameObject.layer == enemyLayer))
        {
            this.DealDamageEvent?.Invoke(collider.gameObject);
            this.CollisionEnterEvent?.Invoke(bullet);
        }

    }

    //public void CheckCollision(Collision2D collider, GameObject bullet)
    //{
    //    //Debug.Log($"checking collision of {collider.gameObject.name} and {bullet}");
    //    if (!collider.gameObject.TryGetComponent(out TeamComponentMono team))
    //        return;
    //    if (this.bulletConfig.isPlayer == team.IsPlayer)
    //        return;

    //    this.DealDamageEvent?.Invoke(collider.gameObject, this.bulletConfig.damage);
    //    //collider.gameObject.GetComponent<HitPointsComponentMono>().TakeDamage(bulletConfig.damage);
    //    //Debug.Log($"{bullet.name} has collided");
    //    this.CollisionEnterEvent?.Invoke(bullet);
    //}

}
