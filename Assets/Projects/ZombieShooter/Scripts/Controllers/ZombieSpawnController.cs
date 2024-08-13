using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class ZombieSpawnController : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _world;
        [SerializeField] private Zombie _zombiePrefab;
        [SerializeField] private AtomicObject _target;

        [SerializeField] private float _spawnInterval;
        [SerializeField] private int _numToSpawn;
        [SerializeField] private int _zombiesOnStage;

        private Pool<Zombie> _zombiePool;

        private AtomicEvent<Zombie> ZombieDeadEvent = new();
        private float _timer;
        private bool _CanSpawn;

        private ZombieInitiateMechanics _initiateMechanics;

        private void Awake()
        {
            _zombiePool = new Pool<Zombie>(_zombiePrefab, _numToSpawn, _parent, _world);
            _initiateMechanics = new ZombieInitiateMechanics();

            ZombieDeadEvent.Subscribe(EnqueueZombie);

        }

        private void Update()
        {
            if (_timer <= 0)
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
            Zombie zombie = _zombiePool.GetObject();
            zombie.transform.position = GetRandomSpawnPoint();

            _initiateMechanics.InitiateZombie(zombie, _target, ZombieDeadEvent);

        }


        public void EnqueueZombie(Zombie zombie)
        {
            _zombiePool.Enqueue(zombie);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            int randomPoint = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomPoint].position;
        }

       

    }
}