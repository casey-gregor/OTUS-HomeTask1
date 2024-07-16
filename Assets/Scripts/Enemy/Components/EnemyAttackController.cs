using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackController : IGameFixedUpdateListener, IDisposable
    {
        private WeaponComponent weaponComponent;
        private EnemyCheckDestinationController checkDestinationComponent;
        private EnemyBulletSpawner bulletSpawner;
        private EnemyConfig enemyConfig;
        private LevelProvider levelProvider;
        private Transform target;
        private EnemyHitPointsController enemyHitPointsComponent;
        private TimerFactory timerFactory;

        private Dictionary<GameObject, Timer> attackingObjects;

        public EnemyAttackController
            (
            LevelProvider levelProvider,
            EnemyBulletSpawner bulletSpawner,
            EnemyCheckDestinationController checkDestinationComponent,
            EnemyConfig enemyConfig,
            DiContainer diContainer,
            WeaponComponent weaponComponent,
            EnemyHitPointsController hitPointsComponent,
            TimerFactory timerFactory
            )
        {
            this.levelProvider = levelProvider;
            this.target = this.levelProvider.player;
            this.bulletSpawner = bulletSpawner;
            this.checkDestinationComponent = checkDestinationComponent;
            this.enemyConfig = enemyConfig;
            this.weaponComponent = weaponComponent;
            this.enemyHitPointsComponent = hitPointsComponent;
            this.timerFactory = timerFactory;


            this.checkDestinationComponent.destinationReachedEvent += HandleReachedAttackPointEvent;
            this.enemyHitPointsComponent.hpEmptyEvent += HandleHPEmptyEvent;

            this.attackingObjects = new Dictionary<GameObject, Timer>();
        }

        private void HandleHPEmptyEvent(GameObject obj)
        {
            this.attackingObjects[obj].Stop();
            this.attackingObjects.Remove(obj);
        }

        private void HandleReachedAttackPointEvent(GameObject attacker)
        {
            if (this.attackingObjects.ContainsKey(attacker))
                return;
            Timer timer = this.timerFactory.CreateTimer();
            this.attackingObjects.Add(attacker, timer);
        }

        private void HandleTimeOver(GameObject attacker)
        {
            Shoot(attacker);
        }

        private void Shoot(GameObject attacker)
        {
            Transform startPosition = this.weaponComponent.GetFirePoint(attacker);
            this.bulletSpawner.ShootBullet(startPosition, target);
            this.attackingObjects[attacker].Set(attacker, enemyConfig.timeBetweenShots, HandleTimeOver);
        }

        public void OnFixedUpdate()
        {
            foreach (GameObject attacker in this.attackingObjects.Keys)
            {
                if (!this.attackingObjects[attacker].TimerRunning)
                {
                    Shoot(attacker);
                }
            }
        }

        public void Dispose()
        {
            this.checkDestinationComponent.destinationReachedEvent -= HandleReachedAttackPointEvent;
            this.enemyHitPointsComponent.hpEmptyEvent -= HandleHPEmptyEvent;
        }
    }
}