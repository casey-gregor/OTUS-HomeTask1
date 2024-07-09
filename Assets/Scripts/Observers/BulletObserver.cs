using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletObserver
    {
        private EnemyBulletSpawnerComponent enemyBulletSpawnerComponent;
        private PlayerBulletSpawnerComponent playerBulletSpawnerComponent;
        private BulletCollisionCheckComponent collisionCheckAgent;
        private LevelBoundsCheckComponent levelBoundsCheckComponent;

        public BulletObserver
            (
            EnemyBulletSpawnerComponent enemyBulletSpawnerComponent, 
            PlayerBulletSpawnerComponent playerBulletSpawnerComponent,
            BulletCollisionCheckComponent collisionCheckAgent, 
            LevelBoundsCheckComponent levelBoundsCheckComponent
            ) 
        {
            this.enemyBulletSpawnerComponent = enemyBulletSpawnerComponent;
            this.playerBulletSpawnerComponent= playerBulletSpawnerComponent;
            this.collisionCheckAgent = collisionCheckAgent;
            this.levelBoundsCheckComponent = levelBoundsCheckComponent;

            this.collisionCheckAgent.CollisionEnterEvent += HandleDisableEvent;
            this.levelBoundsCheckComponent.OnOutOfBounds += HandleDisableEvent;

    }

        private void HandleDisableEvent(GameObject obj)
        {
            this.enemyBulletSpawnerComponent.BulletPool.EnqueueItem(obj);
            this.playerBulletSpawnerComponent.BulletPool.EnqueueItem(obj);
        }
    }
}
