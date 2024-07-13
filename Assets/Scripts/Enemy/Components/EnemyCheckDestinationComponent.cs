using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyCheckDestinationComponent : IGameFixedUpdateListener, IDisposable
    {
        public Vector2 direction { get; private set; }

        public event Action<GameObject> destinationReachedEvent;

        private float proximityValue = 0.25f;
        private EnemyInitializeComponent initializer;

        private Dictionary<GameObject, Transform> objectsToCheck;
        private HashSet<GameObject> keysToRemove;

        public EnemyCheckDestinationComponent(EnemyInitializeComponent enemyInitializer)
        {
            this.initializer = enemyInitializer;

            this.initializer.enemyInitializedEvent += HandleInitializedEvent;

            this.objectsToCheck = new Dictionary<GameObject, Transform>();
            this.keysToRemove = new HashSet<GameObject>();

            IGameListener.Register(this);
        }

        private bool CheckIfReached(GameObject obj)
        {
            this.direction = (Vector2)objectsToCheck[obj].position - (Vector2)obj.transform.position;
            return this.direction.magnitude <= this.proximityValue ? true : false;
        }

        private void HandleInitializedEvent(GameObject obj, Transform attackPosition)
        {
            this.objectsToCheck.Add(obj, attackPosition);
        }

        public void OnFixedUpdate()
        {
            this.keysToRemove.Clear();

            foreach(GameObject key in this.objectsToCheck.Keys)
            {
                if (CheckIfReached(key))
                {
                    this.destinationReachedEvent?.Invoke(key);
                    this.keysToRemove.Add(key);
                }
            }

            foreach(GameObject key in this.keysToRemove)
            {
                this.objectsToCheck.Remove(key);
            }
        }

        public void Dispose()
        {
            this.initializer.enemyInitializedEvent -= HandleInitializedEvent;
        }
    }
}
