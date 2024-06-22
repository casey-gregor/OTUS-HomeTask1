using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private PoolManager _bulletPool;
        private BulletObserver _bulletObserver;
        public Transform target { get; private set; }
        public void Awake()
        {
            _bulletPool = new PoolManager(prefab, initialCount, container);
            _bulletObserver = new BulletObserver(_bulletPool);
        }

        public void ShootBullet(WeaponComponent weapon)
        {
            GameObject bulletObject = _bulletPool.GetItem();
            _bulletObserver.Subscribe(bulletObject);
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