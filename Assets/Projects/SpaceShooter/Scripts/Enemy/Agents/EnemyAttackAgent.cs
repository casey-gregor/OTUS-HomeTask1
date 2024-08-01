using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float timeBetweenShots;

        private bool timerLaunched;

        private WeaponComponent weaponComponent;
        private EnemyMoveAgent moveAgent;
        private BulletSpawner bulletSystem;
        private Timer timer;


        private void OnEnable()
        {
            if (TryGetComponent<WeaponComponent>(out weaponComponent) == false)
                Debug.LogError($"{this.name} is missing WeaponComponent");
            if (TryGetComponent<EnemyMoveAgent>(out moveAgent) == false)
                Debug.LogError($"{this.name} is missing EnemyMoveAgent");
            timer = new Timer(this);
        }

        private bool CanAttack()
        {
            return this.moveAgent.IsReached && IsTargetAlive();
        }
        private bool IsTargetAlive()
        {
            HitPointsComponent targetHitPoints = this.bulletSystem.target?.GetComponent<HitPointsComponent>();
            return targetHitPoints.IsAlive();
        }
        public void SetBulletSystem(BulletSpawner bulletSystem)
        {
            this.bulletSystem = bulletSystem;
        }

        public void OnFixedUpdate()
        {
            if (CanAttack() && !timerLaunched)
            {
                Shoot();
            }
        }

        private void HandleTimeOver()
        {
            
            timer.StopCountdown();
            timer.TimeIsOver -= HandleTimeOver;
            timerLaunched = false;
        }

        private void Shoot()
        {
            this.bulletSystem.ShootBullet(weaponComponent);

            timer.Set(timeBetweenShots);
            timer.TimeIsOver += HandleTimeOver;
            timer.StartCountdown();
            timerLaunched = true;
        }
    }
}