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

        private EnemyFactory _enemyPool;
        private EnemyObserver _enemyObserver;


        private void Awake()
        {
            _enemyPool = new EnemyFactory(prefab, initialCount, container);
            _enemyObserver = new EnemyObserver(_enemyPool);
        }
        private void Start()
        {
            IGameListener.Register(this);
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnEveryNumOfSeconds);
                GameObject enemy = this._enemyPool.GetItem();
                InitilizeEnemy(enemy);
                _enemyObserver.SubscribeToObject(enemy);
            }
        }

        public void InitilizeEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.worldTransform);
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetBulletSystem(bulletSystem);
            bulletSystem.SetTarget(this.character);
        }

        public void OnStart()
        {
            StartCoroutine(SpawnEnemies());
        }

        public void OnPause()
        {
            StopAllCoroutines();
        }

        public void OnResume()
        {
            StartCoroutine(SpawnEnemies());
        }

        public void OnFinish()
        {
            StopAllCoroutines();
        }
    }
}