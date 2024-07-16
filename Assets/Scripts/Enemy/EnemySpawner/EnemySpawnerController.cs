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
        private EnemyHitPointsController hitPoints;
        private Timer timer;
        private DiContainer diContainer;
        private Pool enemyPool;
        private EnemyObserver enemyObserver;

        public event Action<GameObject> enemySpawnedEvent;

        public EnemySpawnerController
            (
            LevelProvider levelProvider,
            EnemySpawnerConfig spawnerConfig,
            EnemyHitPointsController hitPointComponent,
            Timer timer,
            DiContainer diContainer
            )
        {
            this.levelProvider = levelProvider;
            this.enemyContainer = this.levelProvider.enemyContainer;
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

            this.hitPoints.SetSpawnerAndSubscribe(this);
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