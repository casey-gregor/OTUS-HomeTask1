using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private int initialCount = 7;
        [SerializeField] private int spawnEveryNumOfSeconds = 5;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform container;
        [SerializeField] private Transform character;
        [SerializeField] private BulletSpawner bulletSystem;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private EnemyPositions enemyPositions;

        private Pool enemyPool;
        private EnemyObserver enemyObserver;
        private Timer timer;


        private void Awake()
        {
            enemyPool = new Pool(prefab, initialCount, container);
            enemyObserver = new EnemyObserver(enemyPool);
            timer = new Timer(this);
        }
        public void OnStart()
        {
            LaunchTimer();
        }

        private void SpawnEnemy()
        {
            GameObject enemy = this.enemyPool.GetItem();
            InitilizeEnemy(enemy);
            enemyObserver.Subscribe(enemy);
        }
        private void InitilizeEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.worldTransform);
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetBulletSystem(bulletSystem);
            bulletSystem.SetTarget(this.character);
        }
        private void LaunchTimer()
        {
            timer.Set(spawnEveryNumOfSeconds);
            timer.StartCountdown();
            timer.TimeIsOver += HandleTimeOver;
        }
        private void HandleTimeOver()
        {
            SpawnEnemy();
            timer.TimeIsOver -= HandleTimeOver;
            LaunchTimer();
        }
        public void OnPause()
        {
            timer.StopCountdown();
        }

        public void OnResume()
        {
            timer.StartCountdown();
        }

        public void OnFinish()
        {
            timer.StopCountdown();
        }
    }
}