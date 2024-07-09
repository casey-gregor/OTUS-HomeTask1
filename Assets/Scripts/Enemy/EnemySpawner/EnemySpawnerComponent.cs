using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawnerComponent :
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameFinishListener
    {

        private Transform enemyContainer;
        private EnemySpawnerConfig spawnerConfig;
        private EnemyHitPointsComponent hitPoints;
        private TimerService timer;
        private DiContainer diContainer;
        private Pool enemyPool;
        private EnemyObserver enemyObserver;

        public event Action<GameObject> enemySpawnedEvent;

        public EnemySpawnerComponent
            (
            [Inject(Id = IdCollection.enemyContainer)] Transform container,
            EnemySpawnerConfig spawnerConfig,
            EnemyHitPointsComponent hitPointComponent,
            TimerService timer,
            DiContainer diContainer
            )
        {
            this.enemyContainer = container;
            this.diContainer = diContainer;
            this.spawnerConfig = spawnerConfig;
            this.timer = timer;
            this.hitPoints = hitPointComponent;

            this.enemyPool = diContainer.Instantiate<Pool>
                (new object[] 
                {
                    this.spawnerConfig.enemyPrefab,
                    this.spawnerConfig.initialEnemiesCount,
                    this.enemyContainer,
                    this.diContainer
                });

            this.enemyObserver = diContainer.Instantiate<EnemyObserver>
                (new object[] 
                { 
                    this.enemyPool, 
                    this.hitPoints 
                });

            this.hitPoints.SubscribeToSpawner(this);

            IGameListener.Register(this);
        }

        public void OnStart()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            GameObject enemy = this.enemyPool.GetItem();
            this.enemySpawnedEvent?.Invoke(enemy);
            LaunchTimer();
        }
       
        private void LaunchTimer()
        {
            this.timer.Set(this.spawnerConfig.spawnEnemiesEveryNumOfSeconds, HandleTimeOver);
        }

        private void HandleTimeOver()
        {
            SpawnEnemy();
        }

        public void OnPause()
        {
            this.timer.Stop();
        }

        public void OnResume()
        {
            this.timer.Resume();
        }

        public void OnFinish()
        {
            this.timer.Stop();
        }

    }
}