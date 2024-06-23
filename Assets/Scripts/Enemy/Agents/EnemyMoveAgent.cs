using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MoveComponent, IGameFixedUpdateListener
    {
        public bool IsReached { get { return this.isReached; } }

        private bool isReached;
        private Vector2 destination;
        private Rigidbody2D _rigibody2d;
       
        private void OnEnable()
        {
            if (TryGetComponent<Rigidbody2D>(out _rigibody2d) == false)
                Debug.LogError($"{this.name} is missing Rigidbody2D");
        }
       
        private bool CheckIfReached(Vector2 vector)
        {
            if (vector.magnitude <= 0.25f)
            {
                this.isReached = true;
            }
            return this.isReached;
        }
        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isReached = false;
        }

        public void OnFixedUpdate()
        {
            var vector = this.destination - (Vector2)this.transform.position;

            if (CheckIfReached(vector))
                return;

            var direction = vector.normalized * Time.fixedDeltaTime;
            Move(_rigibody2d, direction);
        }
    }
}