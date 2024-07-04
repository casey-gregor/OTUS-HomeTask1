using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawner :
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameFinishListener
    {

        private Transform enemyContainer;
        private Transform character;
        private Transform worldTransform;
        private EnemyPositions enemyPositions;
        private EnemyRandomAttackPositionAgent randomAttackPositionAgent;
        private BulletSpawnerComponent bulletSpawner;
        private Pool enemyPool;
        private EnemyObserver enemyObserver;
        private TimerService timer;
        private DiContainer diContainer;
        private EnemySpawnerConfig config;
        private EnemyHitPointsComponent hitPoints;

        public event Action<GameObject, Transform> enemySpawnedEvent;

        public EnemySpawner(
            [Inject(Id = BindingIds.enemyContainer)] Transform container,
            [Inject(Id = BindingIds.worldTransform)] Transform worldTransform,
            [Inject(Id = BindingIds.playerId)] Transform character,
            DiContainer diContainer,
            EnemyPositions enemyPositions,
            EnemySpawnerConfig spawnerConfig,
            TimerService timer,
            EnemyHitPointsComponent hitPoints
            )
        {
            this.enemyContainer = container;
            this.worldTransform = worldTransform;
            this.character = character;
            this.diContainer = diContainer;
            this.enemyPositions = enemyPositions;
            this.config = spawnerConfig;
            this.timer = timer;
            this.hitPoints = hitPoints;

            this.enemyPool = diContainer.Instantiate<Pool>
                (new object[] {
                    this.config.enemyPrefab,
                    this.config.initialEnemiesCount,
                    this.enemyContainer,
                    this.diContainer
                });
            enemyObserver = diContainer.Instantiate<EnemyObserver>(new object[] { this.enemyPool, this.hitPoints });

            this.hitPoints.SubscribeToSpawner(this);
            //timer = diContainer.Instantiate<TimerService>(new object[] {});
            //timer = new TimerService();

            IGameListener.Register(this);
            //Debug.Log("construct of enemy spawner");
        }

        public void OnStart()
        {
            SpawnEnemy();
            //Debug.Log("launch timer");
        }

        private void SpawnEnemy()
        {
            GameObject enemy = this.enemyPool.GetItem();
            InitilizeEnemy(enemy);
            enemyObserver.Subscribe(enemy);
            LaunchTimer();
        }
        private void InitilizeEnemy(GameObject enemy)
        {
            //Debug.Log("initialize enemy ");
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            //Debug.Log("spawnPosition : " + spawnPosition.transform.position);
            enemy.transform.position = spawnPosition.position;

            Transform attackPosition = this.enemyPositions.RandomAttackPosition();
            //Debug.Log("attack Position : " + attackPosition.transform.position);
            enemySpawnedEvent?.Invoke(enemy, attackPosition);
            enemy.transform.SetParent(this.worldTransform);
            //Debug.Log("enemy position2 : " + enemy.transform.position);
            
        }
        private void LaunchTimer()
        {
            //Debug.Log("In launch timer");
            //Debug.Log("timer time : " + this.config.spawnEnemiesEveryNumOfSeconds);
            timer.Set(this.config.spawnEnemiesEveryNumOfSeconds, HandleTimeOver);
        }
        private void HandleTimeOver()
        {
            //Debug.Log("time over");
            SpawnEnemy();
            //LaunchTimer();
        }
        public void OnPause()
        {
            timer.Stop();
        }

        public void OnResume()
        {
            timer.Resume();
        }

        public void OnFinish()
        {
            timer.Stop();
        }

    }
}