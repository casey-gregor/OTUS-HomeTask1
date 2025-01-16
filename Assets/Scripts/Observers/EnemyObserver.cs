using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyObserver : IDisposable
    {
        private Pool enemyPool;
        private EnemyHitPointsController hitPointsComponent;
        private EnemySpawnerController spawnerController;

        public EnemyObserver(
            Pool pool, 
            EnemyHitPointsController hitPointsComponent, 
            EnemySpawnerController spawnerController)
        {
            this.enemyPool = pool;
            this.hitPointsComponent = hitPointsComponent;
            this.spawnerController = spawnerController;

            hitPointsComponent.hpEmptyEvent += this.ReturnEnemyToPool;
            this.spawnerController.enemySpawnFailed += ReturnEnemyToPool;
        }

        private void ReturnEnemyToPool(GameObject enemyObject)
        {
            enemyPool.EnqueueItem(enemyObject);
        }

        public void Dispose()
        {
            hitPointsComponent.hpEmptyEvent -= this.ReturnEnemyToPool;
        }
    }

}
