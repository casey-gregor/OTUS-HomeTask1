using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CollisionCheckComponent : MonoBehaviour
    {
        [SerializeField] private Bullet bullet;

        public event Action<GameObject> OnCollisionEntered;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent team))
                return;
            if (bullet.BulletConfig.isPlayer == team.IsPlayer)
                return;
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(bullet.BulletConfig.damage);

            bullet.SetIsActive(false);
            this.OnCollisionEntered?.Invoke(this.gameObject);
        }
    }

}
