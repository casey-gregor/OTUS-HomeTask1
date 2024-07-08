using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletInitializeComponent
    {
        private EnemyBulletSpawnerComponent enemyBulletSpawnerComponent;
        private PlayerBulletSpawnerComponent playerBulletSpawnerComponent;
        private Transform worldTransform;

        public event Action<GameObject, Vector2> bulletToMoveEvent;
        
        public BulletInitializeComponent(
            EnemyBulletSpawnerComponent enemyBulletSpawnerComponent,
            PlayerBulletSpawnerComponent playerBulletSpawnerComponent,
            [Inject(Id = BindingIds.worldTransform)] Transform worldTransform
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

        //private void HandleBulletInitializeEvent(GameObject obj, Vector2 startPosition, Vector2 endPosition)
        //{
        //    InitializeBullet(obj, startPosition, endPosition);

        //}

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