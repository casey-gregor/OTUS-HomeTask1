using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{

    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private BulletConfig bulletConfig;
        public bool isActive { get; private set; }
        public Transform Transform { get => transform; }
        public LevelBounds levelBounds { get; private set; }
        public BulletConfig BulletConfig { get { return bulletConfig; } }

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Vector2 velocity;


        private void OnEnable()
        {
            if (TryGetComponent<Rigidbody2D>(out rb) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
            if (TryGetComponent<SpriteRenderer>(out spriteRenderer) == false)
                Debug.LogError($"{this.name} is missing SpriteRenderer"); 
        }

        public void InitializeBullet(Vector2 startPosition, Vector2 direction, LevelBounds levelBounds)
        {
            this.gameObject.layer = (int)bulletConfig.physicsLayer;
            this.levelBounds = levelBounds;
            this.spriteRenderer.color = bulletConfig.color;
            this.transform.position = startPosition;
            this.velocity = direction * bulletConfig.speed;
            this.rb.velocity = velocity;
            SetIsActive(true);
        }

        public void SetIsActive(bool value)
        {
            this.isActive = value;
        }

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