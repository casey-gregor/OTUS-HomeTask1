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
        public LevelBounds _levelBounds { get; private set; }
        public BulletConfig BulletConfig { get { return bulletConfig; } }

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private Vector2 velocity;


        private void OnEnable()
        {
            if (TryGetComponent<Rigidbody2D>(out _rigidbody2D) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
            if (TryGetComponent<SpriteRenderer>(out _spriteRenderer) == false)
                Debug.LogError($"{this.name} is missing SpriteRenderer"); 
        }

        public void InitializeBullet(Vector2 startPosition, Vector2 direction, LevelBounds _levelBounds)
        {
            this.gameObject.layer = (int)bulletConfig.physicsLayer;
            this._levelBounds = _levelBounds;
            this._spriteRenderer.color = bulletConfig.color;
            this.transform.position = startPosition;
            this.velocity = direction * bulletConfig.speed;
            this._rigidbody2D.velocity = velocity;
            SetIsActive(true);
        }

        public void SetIsActive(bool value)
        {
            this.isActive = value;
        }

        public void OnPause()
        {
            this._rigidbody2D.velocity = Vector2.zero;
        }

        public void OnResume()
        {
            this._rigidbody2D.velocity = velocity;
        }

        public void OnFinish()
        {
            this._rigidbody2D.velocity = Vector2.zero;
        }
    }
}