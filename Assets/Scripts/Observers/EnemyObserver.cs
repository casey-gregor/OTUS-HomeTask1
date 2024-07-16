using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyObserver
    {
        private Pool enemyPool;
        private EnemyHitPointsController hitPointsComponent;

        public EnemyObserver(Pool pool, EnemyHitPointsController hitPointsComponent)
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
