using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : IGameFixedUpdateListener
    {
        private Rigidbody2D rb;
        private EnemyCheckDestinationAgent checkDestinationAgent;
        private EnemySpawner spawner;
        private BulletSpawnerComponent bulletSpawner;

        private Dictionary<GameObject, Transform> objectsToMove;

        public EnemyMoveAgent(EnemySpawner enemySpawner, EnemyCheckDestinationAgent checkDestinationAgent)
        {
            this.spawner = enemySpawner;
            this.checkDestinationAgent = checkDestinationAgent;

            this.spawner.enemySpawnedEvent += SpawnEventHandler;
            this.checkDestinationAgent.destinationReachedEvent += DestinationReachedEventHandler;
            objectsToMove = new Dictionary<GameObject, Transform>();

            IGameListener.Register(this);
            //Debug.Log("Enemy move agent register");
        }

        private void SpawnEventHandler(GameObject obj, Transform destination)
        {
            objectsToMove.Add(obj, destination);
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