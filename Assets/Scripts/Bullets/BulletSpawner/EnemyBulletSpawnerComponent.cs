using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyBulletSpawnerComponent
    {
        public BulletConfig BulletConfig => bulletConfig;
        public Pool BulletPool => bulletPool;

        public event Action<GameObject, BulletConfig, Transform, Transform> bulletSpawnedEvent;

        private BulletSpawnerConfig bulletSpawnerConfig;
        private Pool bulletPool;
        private BulletConfig bulletConfig;
        private Transform parent;


        public EnemyBulletSpawnerComponent
            (
            BulletConfig bulletConfig, 
            BulletSpawnerConfig bulletSpawnerConfig, 
            Transform parent, 
            DiContainer diContainer
            )
        { 
            this.bulletSpawnerConfig = bulletSpawnerConfig;
            this.bulletConfig = bulletConfig;
            this.parent = parent;

            this.bulletPool = new Pool(this.bulletSpawnerConfig.prefab, this.bulletSpawnerConfig.initialCount, this.parent, diContainer);

        }


        public void ShootBullet(Transform startPosition, Transform target)
        {
            GameObject bulletObject = this.bulletPool.GetItem();
            bulletSpawnedEvent?.Invoke(bulletObject, this.bulletConfig, startPosition, target);
        }

    }

}
