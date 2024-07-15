using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackComponent : IGameFixedUpdateListener, IDisposable
    {
        private WeaponComponent weaponComponent;
        private EnemyCheckDestinationComponent checkDestinationComponent;
        private EnemyBulletSpawner bulletSpawner;
        private EnemyConfig enemyConfig;
        private LevelProvider levelProvider;
        private Transform target;
        private DiContainer diContainer;
        private EnemyHitPointsComponent enemyHitPointsComponent;

        private Dictionary<GameObject, Timer> attackingObjects;

        public EnemyAttackComponent
            (
            LevelProvider levelProvider,
            EnemyBulletSpawner bulletSpawner,
            EnemyCheckDestinationComponent checkDestinationComponent,
            EnemyConfig enemyConfig,
            DiContainer diContainer,
            WeaponComponent weaponComponent,
            EnemyHitPointsComponent hitPointsComponent
            )
        {
            this.levelProvider = levelProvider;
            this.target = this.levelProvider.player;
            this.bulletSpawner = bulletSpawner;
            this.checkDestinationComponent = checkDestinationComponent;
            this.enemyConfig = enemyConfig;
            this.diContainer = diContainer;
            this.weaponComponent = weaponComponent;
            this.enemyHitPointsComponent = hitPointsComponent;


            this.checkDestinationComponent.destinationReachedEvent += HandleReachedAttackPointEvent;
            this.enemyHitPointsComponent.hpEmptyEvent += HandleHPEmptyEvent;

            this.attackingObjects = new Dictionary<GameObject, Timer>();

            IGameListener.Register(this);
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
            Timer timer = diContainer.Instantiate<Timer>();
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