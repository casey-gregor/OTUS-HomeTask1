using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MoveComponent
    {
        private Rigidbody2D _rigibody2d;
        private void OnEnable()
        {
            if (TryGetComponent<Rigidbody2D>(out _rigibody2d) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
        }
        public bool IsReached
        {
            get { return this.isReached; }
        }

        private Vector2 destination;

        private bool isReached;

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isReached = false;
        }

        private void FixedUpdate()
        {
            if (this.isReached)
            {
                return;
            }
            
            var vector = this.destination - (Vector2) this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            MoveByRigidbodyVelocity(_rigibody2d, direction);
        }
    }
}