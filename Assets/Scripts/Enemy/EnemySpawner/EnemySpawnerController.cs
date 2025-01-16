using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawnerController :
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameFinishListener
    {
        private LevelProvider levelProvider;
        private Transform enemyContainer;
        private EnemySpawnerConfig spawnerConfig;
        private EnemyHitPointsController hitPointsController;
        private Timer timer;
        private DiContainer diContainer;
        private Pool enemyPool;
        private EnemyObserver enemyObserver;

        public event Action<GameObject> enemySpawnedEvent;
        public event Action<GameObject> enemySpawnFailed;

        public EnemySpawnerController
            (
            LevelProvider levelProvider,
            EnemySpawnerConfig spawnerConfig,
            EnemyHitPointsController hitPointControllerComponent,
            Timer timer,
            DiContainer diContainer
            )
        {
            this.levelProvider = levelProvider;
            this.enemyContainer = this.levelProvider.enemyContainer;
            this.diContainer = diContainer;
            this.spawnerConfig = spawnerConfig;
            this.timer = timer;
            this.hitPointsController = hitPointControllerComponent;

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
                    this.hitPointsController,
                    this
                });

            this.hitPointsController.SetSpawnerAndSubscribe(this);
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

        public void EnemySpawnFailed(GameObject enemy)
        {
            enemySpawnFailed?.Invoke(enemy);
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