using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyObserver
    {
        private Pool enemyPool;
        private EnemyHitPointsComponent hitPointsComponent;

        public EnemyObserver(Pool pool, EnemyHitPointsComponent hitPointsComponent)
        {
            this.enemyPool = pool;
            this.hitPointsComponent = hitPointsComponent;

            hitPointsComponent.hpEmptyEvent += this.HandleHPEmptyEvent;
        }

        private void HandleHPEmptyEvent(GameObject enemyObject)
        {
            enemyPool.EnqueueItem(enemyObject);
        }
    }

}
