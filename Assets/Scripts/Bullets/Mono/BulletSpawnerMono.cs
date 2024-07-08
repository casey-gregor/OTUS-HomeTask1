using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSpawnerMono : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform container;

        private Transform worldTransform;
        private LevelBoundsComponent levelBounds;
        private BulletPool bulletPool;
        private BulletObserver bulletObserver;
        private LevelBoundsCheckComponent levelBoundsCheckComponent;
        private BulletCollisionCheckComponent collisionCheckAgent;
        private DiContainer diContainer;


        [Inject] 
        private void Construct(
            LevelBoundsComponent levelBounds, 
            [Inject(Id ="WorldTransform")] Transform worldTransform,
            LevelBoundsCheckComponent levelBoundsCheckComponent,
            BulletCollisionCheckComponent collisionCheckAgent,
            DiContainer diContainer)
        {
            this.levelBounds = levelBounds;
            this.worldTransform = worldTransform;
            this.levelBoundsCheckComponent = levelBoundsCheckComponent;
            this.collisionCheckAgent = collisionCheckAgent;
            this.diContainer = diContainer;
        }
        public Transform target { get; private set; }

        public void Awake()
        {
            //bulletPool = new BulletPool(this.prefab, this.initialCount, this.container, this.diContainer);
            //bulletObserver = new BulletObserver(bulletPool, this.collisionCheckAgent, this.levelBoundsCheckComponent);
        }

        public void ShootBullet(WeaponComponentMono weapon)
        {
            GameObject bulletObject = bulletPool.GetItem();
            bulletObserver.Subscribe(bulletObject);
            bulletObject.transform.SetParent(this.worldTransform);

            var startPosition = weapon.Position;
            Vector2 vector = this.target != null ? (Vector2)this.target.position - startPosition : Vector2.up;
            Vector2 endPosition = weapon.Rotation * vector.normalized;
            BulletMono bullet = bulletObject.GetComponent<BulletMono>();
            bullet.InitializeBullet(startPosition, endPosition, levelBounds);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}