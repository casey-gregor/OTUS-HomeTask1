using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private int initialCount = 7;
        [SerializeField] private int spawnEveryNumOfSeconds = 5;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform container;
        [SerializeField] private Transform character;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private EnemyPositions enemyPositions;

        private PoolManager _enemyPool;
        private EnemyObserver _enemyObserver;
        private Timer _timer;


        private void Awake()
        {
            _enemyPool = new PoolManager(prefab, initialCount, container);
            _enemyObserver = new EnemyObserver(_enemyPool);
            _timer = new Timer(this);
        }
        public void OnStart()
        {
            LaunchTimer();
        }

        private void SpawnEnemy()
        {
            GameObject enemy = this._enemyPool.GetItem();
            InitilizeEnemy(enemy);
            _enemyObserver.Subscribe(enemy);
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
            _timer.Set(spawnEveryNumOfSeconds);
            _timer.StartCountdown();
            _timer.TimeIsOver += HandleTimeOver;
        }
        private void HandleTimeOver()
        {
            SpawnEnemy();
            _timer.TimeIsOver -= HandleTimeOver;
            LaunchTimer();
        }
        public void OnPause()
        {
            _timer.StopCountdown();
        }

        public void OnResume()
        {
            _timer.StartCountdown();
        }

        public void OnFinish()
        {
            _timer.StopCountdown();
        }
    }
}