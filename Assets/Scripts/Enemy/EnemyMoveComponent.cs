using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveComponent : IGameFixedUpdateListener
    {
        private EnemySpawnerComponent spawner;
        private EnemyCheckDestinationComponent checkDestinationComponent;

        private Dictionary<GameObject, Transform> objectsToMove;

        public EnemyMoveComponent(EnemySpawnerComponent enemySpawner, EnemyCheckDestinationComponent checkDestinationComponent)
        {
            this.spawner = enemySpawner;
            this.checkDestinationComponent = checkDestinationComponent;

            this.spawner.enemySpawnedEvent += HandleSpawnEvent;
            this.checkDestinationComponent.destinationReachedEvent += DestinationReachedEventHandler;

            objectsToMove = new Dictionary<GameObject, Transform>();
            IGameListener.Register(this);
        }

        private void HandleSpawnEvent(GameObject spawnedObject, Transform destination)
        {
            objectsToMove.Add(spawnedObject, destination);
        }

        private void DestinationReachedEventHandler(GameObject obj)
        {
            objectsToMove.Remove(obj);
        }

        public void Move(Rigidbody2D rb, Vector2 vector, float speed)
        {
            Vector2 nextPosition = rb.position + vector * speed;
            rb.MovePosition(nextPosition);
        }

        public void OnFixedUpdate()
        {
            foreach(GameObject obj in objectsToMove.Keys) 
            {
                Vector2 vector = objectsToMove[obj].position - obj.transform.position;
                Vector2 direction = vector.normalized * Time.fixedDeltaTime;
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Move(rb, direction, 5f);
            }
        }
    }
}