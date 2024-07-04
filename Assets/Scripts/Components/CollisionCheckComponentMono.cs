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
            if (!collision.gameObject.TryGetComponent(out TeamComponent team))
                return;
            if (bullet.BulletConfig.isPlayer == team.IsPlayer)
                return;
            if (collision.gameObject.TryGetComponent(out HitPointsComponentMono hitPoints))
                hitPoints.TakeDamage(bullet.BulletConfig.damage);

            this.OnCollisionEntered?.Invoke(this.gameObject);
        }
    }

}
