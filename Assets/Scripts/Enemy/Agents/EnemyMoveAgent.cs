using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MoveComponent, IGameFixedUpdateListener
    {
        private Rigidbody2D _rigibody2d;
        private void OnEnable()
        {
            IGameListener.Register(this);

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

        public void OnFixedUpdate()
        {
            //Debug.Log("Enemy move");
            var vector = this.destination - (Vector2)this.transform.position;

            if (CheckIfReached(vector))
                return;

            var direction = vector.normalized * Time.fixedDeltaTime;
            MoveByRigidbodyVelocity(_rigibody2d, direction);
        }

        private bool CheckIfReached(Vector2 vector)
        {
           
            if (vector.magnitude <= 0.25f)
            {
                this.isReached = true;
            }
            return this.isReached;

        }

        private void OnDisable()
        {
            IGameListener.Unregister(this);
        }
    }
}