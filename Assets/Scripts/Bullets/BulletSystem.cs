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

        private BulletFactory _bulletFactory;
        private BulletObserver _bulletObserver;
        public Transform target { get; private set; }

        public void Awake()
        {
            _bulletFactory = new BulletFactory(prefab, initialCount, container);
            _bulletObserver = new BulletObserver(_bulletFactory);
        }

        public void ShootBullet(WeaponComponent weapon)
        {
            GameObject bullet = _bulletFactory.GetItem();
            _bulletObserver.SubscribeToBullet(bullet);
            bullet.transform.SetParent(this.worldTransform);

            var startPosition = weapon.Position;
            Vector2 vector = this.target != null ? (Vector2)this.target.position - startPosition : Vector2.up;
            Vector2 endPosition = weapon.Rotation * vector.normalized;

            bullet.GetComponent<Bullet>().InitializeBullet(startPosition, endPosition, levelBounds);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}