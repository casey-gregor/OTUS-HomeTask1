using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyInitializeComponent : IDisposable
    {
        public event Action<GameObject, Transform> enemyInitializedEvent;
        
        private EnemySpawnerComponent enemySpawner;
        private EnemyPositionsComponent enemyPositions;
        private LevelProvider levelProvider;
        private Transform worldTransform;

        public EnemyInitializeComponent(
            EnemySpawnerComponent enemySpawner, 
            EnemyPositionsComponent enemyPositions,
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
