using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CollisionCheckWrapper : MonoBehaviour
    {
        private BulletCollisionCheckComponent collisionCheckComponent;

        [Inject]
        public void Construct(BulletCollisionCheckComponent collisionCheckAgent)
        {
            //Debug.Log("CollisionCheckWrapper construct");
            this.collisionCheckComponent = collisionCheckAgent;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"{this.gameObject} collides with {collision.gameObject.name}");
            collisionCheckComponent.CheckCollision(collision, this.gameObject);
        }
    }

}
