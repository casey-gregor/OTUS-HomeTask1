using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CollisionCheckWrapper : MonoBehaviour
    {
        private BulletCollisionCheckController collisionCheckComponent;

        [Inject]
        public void Construct(BulletCollisionCheckController collisionCheckAgent)
        {
            this.collisionCheckComponent = collisionCheckAgent;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.collisionCheckComponent.CheckCollision(collision, this.gameObject);
        }
    }

}
