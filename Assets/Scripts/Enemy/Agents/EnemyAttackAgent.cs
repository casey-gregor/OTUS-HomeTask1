using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : IGameFixedUpdateListener
    {
        private EnemyCheckDestinationAgent checkDestinationAgent;
        private BulletSpawnerComponent bulletSpawner;
        private EnemyConfig config;
        private TickableManager tickableManager;
        private Transform target;
        private DiContainer diContainer;

        private Dictionary<GameObject, TimerService> attackingObjects;

        public EnemyAttackAgent(
            BulletSpawnerComponent bulletSpawner,
            EnemyCheckDestinationAgent checkDestinationAgent,
            EnemyConfig enemyConfig,
            DiContainer diContainer,
            TickableManager tickableManager,
            [Inject(Id = BindingIds.playerId)]Transform target)
        {
            this.bulletSpawner = bulletSpawner;
            this.checkDestinationAgent = checkDestinationAgent;
            this.config = enemyConfig;
            this.diContainer = diContainer;
            this.tickableManager = tickableManager;

            attackingObjects = new Dictionary<GameObject, TimerService>();

            this.checkDestinationAgent.destinationReachedEvent += HandleReachedAttackPointEvent;

            IGameListener.Register(this);
            //Debug.Log("Enemy attack agent register");
            this.target = target;
        }

        private void HandleReachedAttackPointEvent(GameObject obj)
        {
            if (attackingObjects.ContainsKey(obj))
                return;
            TimerService timer = diContainer.Instantiate<TimerService>();
            attackingObjects.Add(obj, timer);
        }
        private bool CanAttack()
        {
            return IsTargetAlive();
        }

        private bool IsTargetAlive()
        {
            HitPointsComponentMono targetHitPoints = target?.GetComponent<HitPointsComponentMono>();
            return targetHitPoints.IsAlive();
        }


        private void HandleTimeOver(GameObject obj)
        {
            //Debug.Log("shoot");
            Shoot(obj);
        }

        private void Shoot(GameObject obj)
        {
            this.bulletSpawner.ShootBullet(obj);
            attackingObjects[obj].Set(obj, config.timeBetweenShots, HandleTimeOver);
        }

        public void OnFixedUpdate()
        {
            if (CanAttack())
            {
                foreach(GameObject obj in attackingObjects.Keys)
                {
                    if (!attackingObjects[obj].timerRunning)
                    {
                        Shoot(obj);
                    }
                }

            }
        }
    }

    //public sealed class EnemyAttackAgent : IGameFixedUpdateListener
    //{
    //    private WeaponComponent weaponComponent;
    //    private EnemyMoveAgent moveAgent;
    //    private EnemyCheckDestinationAgent checkDestinationAgent;
    //    private BulletSpawnerComponent bulletSpawner;
    //    private EnemyConfig config;
    //    private TimerService timer;

    //    private bool timerLaunched = false;

    //    public EnemyAttackAgent(
    //        BulletSpawnerComponent bulletSpawner, 
    //        WeaponComponent weaponComponent,
    //        EnemyMoveAgent enemyMoveAgent,
    //        EnemyCheckDestinationAgent checkDestinationAgent,
    //        EnemyConfig enemyConfig, 
    //        TimerService timer)
    //    {
    //        this.bulletSpawner = bulletSpawner;
    //        this.weaponComponent = weaponComponent;
    //        this.moveAgent = enemyMoveAgent;
    //        this.checkDestinationAgent = checkDestinationAgent;
    //        this.config = enemyConfig;
    //        this.timer = timer;

    //        IGameListener.Register(this);
    //        Debug.Log("Enemy attack agent register");
    //    }

    //    private bool CanAttack()
    //    {
    //        return this.checkDestinationAgent.IsReached && IsTargetAlive();
    //    }

    //    private bool IsTargetAlive()
    //    {
    //        HitPointsComponent targetHitPoints = this.bulletSpawner.target?.GetComponent<HitPointsComponent>();
    //        return targetHitPoints.IsAlive();
    //    }

    //    public void SetBulletSystem(BulletSpawnerComponent bulletSpawner)
    //    {
    //        this.bulletSpawner = bulletSpawner;
    //    }

    //    private void HandleTimeOver()
    //    {
    //        timerLaunched = false;
    //    }

    //    private void Shoot()
    //    {
    //        this.bulletSpawner.ShootBullet(weaponComponent);

    //        timer.Set(config.timeBetweenShots, HandleTimeOver);
    //        timerLaunched = true;
    //    }

    //    public void OnFixedUpdate()
    //    {
    //        if (CanAttack() && !timerLaunched)
    //        {

    //            Shoot();
    //        }
    //    }
    //}
}