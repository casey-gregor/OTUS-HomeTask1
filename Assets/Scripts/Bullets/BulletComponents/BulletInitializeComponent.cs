using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletInitializeComponent
    {
        public event Action<GameObject, Vector2> bulletToMoveEvent;
        
        private EnemyBulletSpawnerComponent enemyBulletSpawnerComponent;
        private PlayerBulletSpawnerComponent playerBulletSpawnerComponent;
        private Transform worldTransform;
        
        public BulletInitializeComponent
            (
            EnemyBulletSpawnerComponent enemyBulletSpawnerComponent,
            PlayerBulletSpawnerComponent playerBulletSpawnerComponent,
            [Inject(Id = IdCollection.worldTransform)] Transform worldTransform
            )
        {
            this.enemyBulletSpawnerComponent = enemyBulletSpawnerComponent;
            this.playerBulletSpawnerComponent = playerBulletSpawnerComponent;
            this.worldTransform = worldTransform;

            this.enemyBulletSpawnerComponent.bulletSpawnedEvent += HandleBulletSpawnEvent;
            this.playerBulletSpawnerComponent.bulletSpawnedEvent += HandleBulletSpawnEvent;
        }


        private void HandleBulletSpawnEvent(GameObject obj, BulletConfig bulletConfig, Transform startPosition, Transform target)
        {
            InitializeBullet(obj, bulletConfig, startPosition, target);
        }

        public void InitializeBullet(GameObject bulletObj, BulletConfig bulletConfig, Transform startPosition, Transform target)
        {
            bulletObj.layer = (int)bulletConfig.physicsLayer;
            bulletObj.GetComponent<SpriteRenderer>().color = bulletConfig.color;
            bulletObj.GetComponent<Transform>().position = startPosition.position;

            Vector2 vector = target != null ? (Vector2)target.position - (Vector2)startPosition.position : Vector2.up;
            Vector2 endPosition = startPosition.rotation * vector.normalized;

            Vector2 velocity = endPosition * bulletConfig.speed;
            bulletObj.transform.SetParent(this.worldTransform);

            bulletObj.GetComponent<Rigidbody2D>().velocity = velocity;

            bulletToMoveEvent?.Invoke(bulletObj, velocity);
        }


    }
}