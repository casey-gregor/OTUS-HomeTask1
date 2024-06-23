using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float timeBetweenShots;

        private WeaponComponent _weaponComponent;
        private EnemyMoveAgent _moveAgent;
        private BulletSystem _bulletSystem;
        private Timer timer;

        private float _currentTime;
        private bool timerLaunched;

        private void OnEnable()
        {
            if (TryGetComponent<WeaponComponent>(out _weaponComponent) == false)
                Debug.LogError($"{this.name} is missing WeaponComponent");
            if (TryGetComponent<EnemyMoveAgent>(out _moveAgent) == false)
                Debug.LogError($"{this.name} is missing EnemyMoveAgent");
            timer = new Timer(this);
        }

        private bool CanAttack()
        {
            return this._moveAgent.IsReached && IsTargetAlive();
        }
        private bool IsTargetAlive()
        {
            HitPointsComponent targetHitPoints = this._bulletSystem.target?.GetComponent<HitPointsComponent>();
            return targetHitPoints.IsAlive();
        }
        public void SetBulletSystem(BulletSystem bulletSystem)
        {
            this._bulletSystem = bulletSystem;
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
            this._bulletSystem.ShootBullet(_weaponComponent);

            timer.Set(timeBetweenShots);
            timer.TimeIsOver += HandleTimeOver;
            timer.StartCountdown();
            timerLaunched = true;
        }
    }
}