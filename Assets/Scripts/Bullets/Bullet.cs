using System;
using UnityEngine;

namespace ShootEmUp
{

    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletConfig bulletConfig;

        private bool isActive;
        public Transform Transform { get => transform; }
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private LevelBounds _levelBounds;

        public event Action<GameObject> OnCollisionEntered;
        public event Action<GameObject> OnOutOfBounds;

        private void OnEnable()
        {
            if (TryGetComponent<Rigidbody2D>(out _rigidbody2D) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
            if (TryGetComponent<SpriteRenderer>(out _spriteRenderer) == false)
                Debug.LogError($"{this.name} is missing SpriteRenderer");
        }

        public void InitializeBullet(Vector2 startPosition, Vector2 direction, LevelBounds _levelBounds)
        {
            SetPhysicsLayer((int)bulletConfig.physicsLayer);
            SetLevelBounds(_levelBounds);
            SetColor(bulletConfig.color);
            SetStartPosition(startPosition);
            SetVelocity(direction*bulletConfig.speed);
            SetIsActive(true);
        }

        private void FixedUpdate()
        {
            if (!isActive)
                return;
            if (!_levelBounds.InBounds(this.transform.position))
            {
                this.OnOutOfBounds?.Invoke(this.gameObject);
                isActive = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!collision.gameObject.TryGetComponent(out TeamComponent team))
                return;
            if (this.bulletConfig.isPlayer == team.IsPlayer)
                return;
            if(collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(this.bulletConfig.damage);

            isActive = false;
            this.OnCollisionEntered?.Invoke(this.gameObject);
        }

        public void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.velocity = velocity;
        }
        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }
        public void SetStartPosition(Vector3 position)
        {
            this.transform.position = position;
        }
        public void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }
        public void SetLevelBounds(LevelBounds levelBounds)
        {
            this._levelBounds = levelBounds;
        }
        public void SetIsActive(bool value)
        {
            this.isActive = value;
        }

    }
}