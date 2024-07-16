using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInitializeController : IDisposable
    {
        public event Action<GameObject, Transform> enemyInitializedEvent;
        
        private EnemySpawnerController enemySpawner;
        private EnemyPositionsController enemyPositions;
        private LevelProvider levelProvider;
        private Transform worldTransform;

        public EnemyInitializeController(
            EnemySpawnerController enemySpawner, 
            EnemyPositionsController enemyPositions,
            LevelProvider levelProvider)
        {
            this.enemySpawner = enemySpawner;
            this.levelProvider = levelProvider;
            this.enemyPositions = enemyPositions;
            this.worldTransform = this.levelProvider.worldTransform;
            
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

        public void Dispose()
        {
            this.enemySpawner.enemySpawnedEvent -= HandleEnemySpawnedEvent;
        }
    }

}
