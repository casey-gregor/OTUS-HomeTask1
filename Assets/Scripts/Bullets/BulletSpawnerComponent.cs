using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSpawnerComponent
    {
       
        private BulletSpawnerConfig config;
        private Transform worldTransform;
        private Transform parent;
        private LevelBoundsComponent levelBounds;
        private BulletPool bulletPool;
        private BulletObserver bulletObserver;
        private LevelBoundsCheckComponent levelBoundsCheckComponent;
        private CollisionCheckAgent collisionCheckAgent;
        private DiContainer diContainer;
        private Transform target;
        private WeaponComponent weapon;

        public event Action<GameObject> bulletSpawnEvent;
        public event Action<GameObject, Vector2, Vector2> bulletInitializeEvent;

        public BulletSpawnerComponent(
            LevelBoundsComponent levelBounds,
            [Inject(Id = "WorldTransform")] Transform worldTransform,
            DiContainer diContainer,
            BulletSpawnerConfig bulletSpawnerConfig,
            LevelBoundsCheckComponent levelBoundsCheckComponent,
            CollisionCheckAgent collisionCheckAgent,
            Transform parent,
            [Inject(Id = BindingIds.playerId)] Transform target,
            WeaponComponent weapon)
        {
            this.levelBounds = levelBounds;
            this.worldTransform = worldTransform;
            this.diContainer = diContainer;
            this.config = bulletSpawnerConfig;
            this.levelBoundsCheckComponent = levelBoundsCheckComponent;
            this.collisionCheckAgent = collisionCheckAgent;
            this.parent = parent;
            //Debug.Log("parent : " + this.parent.name);
            this.target = target;

            this.bulletPool = new BulletPool(this.config.prefab, this.config.initialCount, this.parent, this.diContainer);
            this.bulletObserver = new BulletObserver(bulletPool, this.collisionCheckAgent, this.levelBoundsCheckComponent);
            this.levelBoundsCheckComponent.SubscribeToSpawner(this);
            this.weapon = weapon;
        }


        public void ShootBullet(GameObject enemyObj)
        {
            GameObject bulletObject = bulletPool.GetItem();
            //Debug.Log("bulletSpawner got : " + bulletObject.name);
            bulletObserver.Subscribe(bulletObject);
            bulletObject.transform.SetParent(this.worldTransform);

            Vector2 startPosition = this.weapon.GetFirePoint(enemyObj).position;
            Vector2 vector = this.target != null ? (Vector2)this.target.position - startPosition : Vector2.up;
            Vector2 endPosition = this.weapon.Rotation * vector.normalized;
            bulletObject.TryGetComponent<BulletMono>(out BulletMono bullet);
            if(bullet != null)
            {
                bullet.InitializeBullet(startPosition, endPosition, levelBounds);
            }
            else
            {
                bulletInitializeEvent?.Invoke(bulletObject, startPosition, endPosition);
            }
            bulletSpawnEvent?.Invoke(bulletObject);
            //Debug.Log($"{bulletObject.name} spawned");
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
