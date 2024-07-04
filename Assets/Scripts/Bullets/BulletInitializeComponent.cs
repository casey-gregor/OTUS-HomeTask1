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
        
        private BulletSpawnerComponent bulletSpawnerComponent;
        private BulletConfig bulletConfig;
        private LevelBoundsMono levelBounds;

        public event Action<GameObject, Vector2> bulletToMoveEvent;
        
        public BulletInitializeComponent(
            BulletSpawnerComponent bulletSpawnerComponent,
            BulletConfig bulletConfig
           )
        {
            this.bulletSpawnerComponent = bulletSpawnerComponent;
            this.bulletConfig = bulletConfig;

            this.bulletSpawnerComponent.bulletInitializeEvent += HandleBulletInitializeEvent;
        }

        private void HandleBulletInitializeEvent(GameObject obj, Vector2 startPosition, Vector2 endPosition)
        {
            InitializeBullet(obj, startPosition, endPosition);
            
        }

        public void InitializeBullet(GameObject bulletObj, Vector2 startPosition, Vector2 direction)
        {
            bulletObj.layer = (int)this.bulletConfig.physicsLayer;
            bulletObj.GetComponent<SpriteRenderer>().color = bulletConfig.color;
            bulletObj.GetComponent<Transform>().position = startPosition;

            Vector2 velocity = direction * bulletConfig.speed;
            bulletObj.GetComponent<Rigidbody2D>().velocity = velocity;
            
            bulletToMoveEvent?.Invoke(bulletObj, velocity);
        }


    }
}