using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletObserver : IDisposable
    {
        private EnemyBulletSpawner enemyBulletSpawnerComponent;
        private PlayerBulletSpawner playerBulletSpawnerComponent;
        private BulletCollisionCheckController collisionCheckAgent;
        private LevelBoundsCheckController levelBoundsCheckComponent;

        public BulletObserver
            (
            EnemyBulletSpawner enemyBulletSpawnerComponent, 
            PlayerBulletSpawner playerBulletSpawnerComponent,
            BulletCollisionCheckController collisionCheckAgent, 
            LevelBoundsCheckController levelBoundsCheckComponent
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
        public void Dispose()
        {
            this.collisionCheckAgent.CollisionEnterEvent -= HandleDisableEvent;
            this.levelBoundsCheckComponent.OnOutOfBounds -= HandleDisableEvent;
        }

    }
}
