using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyInitializeComponent
    {
        public event Action<GameObject, Transform> enemyInitializedEvent;
        
        private EnemySpawnerComponent enemySpawner;
        private EnemyPositionsComponent enemyPositions;
        private Transform worldTransform;

        public EnemyInitializeComponent(
            EnemySpawnerComponent enemySpawner, 
            EnemyPositionsComponent enemyPositions,
            [Inject(Id = IdCollection.worldTransform)] Transform worldTransform)
        {
            this.enemySpawner = enemySpawner;
            this.worldTransform = worldTransform;
            this.enemyPositions = enemyPositions;
            
            this.enemySpawner.enemySpawnedEvent += HandleEnemySpawnedEvent;
        }

        private void HandleEnemySpawnedEvent(GameObject enemyObject)
        {
            InitilizeEnemy(enemyObject);
        }
        private void InitilizeEnemy(GameObject enemyObject)
        {
            Transform spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemyObject.transform.position = spawnPosition.position;

            Transform attackPosition = this.enemyPositions.RandomAttackPosition();

            this.enemyInitializedEvent?.Invoke(enemyObject, attackPosition);

            enemyObject.transform.SetParent(this.worldTransform);
        }
    }

}
