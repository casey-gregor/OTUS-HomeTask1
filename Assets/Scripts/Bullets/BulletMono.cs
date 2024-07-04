using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletMono : MonoBehaviour, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private BulletConfig bulletConfig;
        public bool isActive { get; private set; }
        public Transform Transform { get => transform; }
        public LevelBoundsComponent levelBounds { get; private set; }
        public BulletConfig BulletConfig { get { return bulletConfig; } }

        //public CollisionCheckAgent collisionCheckAgent { get; private set; }

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

        //[Inject]
        //public void Construct(CollisionCheckAgent collisionCheckAgent)
        //{
        //    this.collisionCheckAgent = collisionCheckAgent;
        //}

        public void InitializeBullet(Vector2 startPosition, Vector2 direction, LevelBoundsComponent levelBounds)
        {
            this.gameObject.layer = (int)bulletConfig.physicsLayer;
            this.levelBounds = levelBounds;
            this.spriteRenderer.color = bulletConfig.color;
            this.transform.position = startPosition;
            this.velocity = direction * bulletConfig.speed;
            this.rb.velocity = velocity;
            //SetIsActive(true);
            //Debug.Log($"{this.name} is active : " + this.isActiveAndEnabled);
        }

        public void SetIsActive(bool value)
        {
            Debug.Log("in SetIsActive incoming value is : " + value);
            this.isActive = value;
            Debug.Log("in SetIsActive isActive : " + this.isActive);
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
