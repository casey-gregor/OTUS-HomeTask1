using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CollisionCheckWrapper : MonoBehaviour
    {
        private CollisionCheckAgent collisionCheckAgent;

        [Inject]
        public void Construct(CollisionCheckAgent collisionCheckAgent)
        {
            //Debug.Log("CollisionCheckWrapper construct");
            this.collisionCheckAgent = collisionCheckAgent;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log($"{this.gameObject} collides with {collision.gameObject.name}");
            collisionCheckAgent.CheckCollision(collision, this.gameObject);
        }
    }

}
