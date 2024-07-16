using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveController : IGameFixedUpdateListener, IDisposable
    {
        private EnemyInitializeController initializer;
        private EnemyCheckDestinationController checkDestinationComponent;

        private Dictionary<GameObject, Transform> objectsToMove;

        public EnemyMoveController
            (
            EnemyInitializeController enemyInitializer, 
            EnemyCheckDestinationController checkDestinationComponent
            )
        {
            this.initializer = enemyInitializer;
            this.checkDestinationComponent = checkDestinationComponent;

            this.initializer.enemyInitializedEvent += HandleSpawnEvent;
            this.checkDestinationComponent.destinationReachedEvent += DestinationReachedEventHandler;

            this.objectsToMove = new Dictionary<GameObject, Transform>();
        }

        private void HandleSpawnEvent(GameObject spawnedObject, Transform destination)
        {
            this.objectsToMove.Add(spawnedObject, destination);
        }

        private void DestinationReachedEventHandler(GameObject obj)
        {
            this.objectsToMove.Remove(obj);
        }

        public void Move(Rigidbody2D rb, Vector2 vector, float speed)
        {
            Vector2 nextPosition = rb.position + vector * speed;
            rb.MovePosition(nextPosition);
        }

        public void OnFixedUpdate()
        {
            foreach(GameObject obj in this.objectsToMove.Keys) 
            {
                Vector2 vector = this.objectsToMove[obj].position - obj.transform.position;
                Vector2 direction = vector.normalized * Time.fixedDeltaTime;
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Move(rb, direction, 5f);
            }
        }

        public void Dispose()
        {
            this.initializer.enemyInitializedEvent -= HandleSpawnEvent;
            this.checkDestinationComponent.destinationReachedEvent -= DestinationReachedEventHandler;
        }
    }
}