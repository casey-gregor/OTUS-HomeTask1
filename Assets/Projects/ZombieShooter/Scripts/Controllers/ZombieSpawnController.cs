using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class ZombieSpawnController : MonoBehaviour
    {
        [HideInInspector] public AtomicVariable<int> ZombiesAlive = new();

        private AtomicEvent<Zombie> EnqueueEvent = new();
        private AtomicEvent InitiateEvent = new();
        private AtomicVariable<Zombie> _zombie = new();
        
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _world;
        [SerializeField] private Zombie _zombiePrefab;
        [SerializeField] private AtomicObject _target;

        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _numToSpawn;
        [SerializeField] private int _zombiesOnStage;

        private Pool<Zombie> _zombiePool;

        private float _timer;
        private bool _CanSpawn;

        private ZombieInitiateMechanics _initiateMechanics;

        private void Awake()
        {
            _zombiePool = new Pool<Zombie>(_zombiePrefab, _numToSpawn, _parent, _world);
            _initiateMechanics = new ZombieInitiateMechanics(_zombie, _target, InitiateEvent, EnqueueEvent);

            ZombiesAlive.Value = _zombiesOnStage;

            EnqueueEvent.Subscribe(EnqueueZombie);
        }

        private void Update()
        {
            if (_timer <= 0 && !_target.GetVariable<bool>(CharacterAPIKeys.IS_DEAD).Value)
            {
                _CanSpawn = true;
            }

            SpawnZombie();

        }

        private void SpawnZombie()
        {
            if (_CanSpawn && _zombiesOnStage > 0)
            {
                InstantiateZombie();

                _CanSpawn = false;
                _timer = _spawnInterval;
                _zombiesOnStage --;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        private void InstantiateZombie()
        {
            _zombie.Value = _zombiePool.GetObject();
            _zombie.Value.transform.position = GetRandomSpawnPoint();

            InitiateEvent.Invoke();
        }


        public void EnqueueZombie(Zombie zombie)
        {
            _zombiePool.Enqueue(zombie);
            ZombiesAlive.Value--;
        }

        private Vector3 GetRandomSpawnPoint()
        {
            int randomPoint = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomPoint].position;
        }

       

    }
}