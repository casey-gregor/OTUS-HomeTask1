using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ShootEmUp
{
    public sealed class Bullet : IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        public bool isActive { get; private set; }
        private BulletConfigsHolder bulletConfigs;
        private Transform transform;
        public LevelBoundsMono levelBounds { get; private set; }
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private BulletSpawnerComponent spawner;
        private Vector2 velocity;
        public BulletConfigsHolder BulletConfigs { get { return bulletConfigs; } }
        public GameObject bulletObj { get; private set; }

        public Bullet(
            BulletConfigsHolder bulletConfigs, 
            Transform transform, 
            LevelBoundsMono levelBounds, 
            Rigidbody2D rb, 
            SpriteRenderer spriteRenderer)
        {
            this.bulletConfigs = bulletConfigs;
            this.transform = transform;
            this.levelBounds = levelBounds;
            this.rb = rb;
            this.spriteRenderer = spriteRenderer;

            this.bulletObj = this.transform.gameObject;

            IGameListener.Register(this);
        }


        public void InitializeBullet(Vector2 startPosition, Vector2 direction, LevelBoundsMono levelBounds)
        {
            this.bulletObj.layer = (int)bulletConfigs.enemyBulletConfig.physicsLayer;
            this.levelBounds = levelBounds;
            this.spriteRenderer.color = bulletConfigs.enemyBulletConfig.color;
            this.transform.position = startPosition;
            this.velocity = direction * bulletConfigs.enemyBulletConfig.speed;
            this.rb.velocity = velocity;
        }

        //public void SetIsActive(bool value)
        //{
        //    //Debug.Log("in SetIsActive incoming value is : " + value);
        //    this.isActive = value;
        //    //Debug.Log("in SetIsActive isActive : " + this.isActive);
        //}

        public void OnPause()
        {
            this.rb.velocity = Vector2.zero;
        }

        public void OnResume()
        {
            this.rb.velocity = velocity;
        }

        public void OnFinish()
        {
            this.rb.velocity = Vector2.zero;
        }
    }
}