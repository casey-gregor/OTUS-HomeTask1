using System;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletCollisionCheckController
    {
        public event Action<GameObject> CollisionEnterEvent;
        public event Action<GameObject> DealDamageEvent;

        private int playerLayer;
        private int enemyLayer;
        private int playerBulletLayer;
        private int enemyBulletLayer;

        public BulletCollisionCheckController()
        {

            this.playerLayer = LayerMask.NameToLayer(IdCollection.playerLayer);
            this.enemyLayer = LayerMask.NameToLayer(IdCollection.enemyLayer);
            this.playerBulletLayer = LayerMask.NameToLayer(IdCollection.playerBulletLayer);
            this.enemyBulletLayer = LayerMask.NameToLayer(IdCollection.enemyBulletLayer);

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

    }

}
