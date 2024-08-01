using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        public Transform target { get; private set; }

        private Pool bulletPool;
        private BulletObserver bulletObserver;
        public void Awake()
        {
            bulletPool = new Pool(prefab, initialCount, container);
            bulletObserver = new BulletObserver(bulletPool);
        }

        public void ShootBullet(WeaponComponent weapon)
        {
            GameObject bulletObject = bulletPool.GetItem();
            bulletObserver.Subscribe(bulletObject);
            bulletObject.transform.SetParent(this.worldTransform);

            var startPosition = weapon.Position;
            Vector2 vector = this.target != null ? (Vector2)this.target.position - startPosition : Vector2.up;
            Vector2 endPosition = weapon.Rotation * vector.normalized;
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.InitializeBullet(startPosition, endPosition, levelBounds);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}