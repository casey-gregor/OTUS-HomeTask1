using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CollisionCheckComponentMono : MonoBehaviour
    {
        private Bullet bullet;

        private void Construct(Bullet bullet)
        {
            this.bullet = bullet;
        }

        public event Action<GameObject> OnCollisionEntered;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponentMono team))
                return;
            if (bullet.BulletConfigs.enemyBulletConfig.isPlayer == team.IsPlayer)
                return;
            if (collision.gameObject.TryGetComponent(out PlayerHitPointsComponent hitPoints))
                //hitPoints.TakeDamage(bullet.BulletConfig.damage);

            this.OnCollisionEntered?.Invoke(this.gameObject);
        }
    }

}
