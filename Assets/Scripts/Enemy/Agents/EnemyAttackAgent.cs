using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float timeBetweenShots;

        WeaponComponent _weaponComponent;
        EnemyMoveAgent _moveAgent;
        BulletSystem _bulletSystem;

        private float _currentTime;

        private void Awake()
        {
            if (TryGetComponent<WeaponComponent>(out _weaponComponent) == false)
                Debug.LogError($"{this.name} is missing WeaponComponent");
            if (TryGetComponent<EnemyMoveAgent>(out _moveAgent) == false)
                Debug.LogError($"{this.name} is missing EnemyMoveAgent");

        }

        private void FixedUpdate()
        {
            if (CanAttack())
            {
                this._currentTime -= Time.fixedDeltaTime;
                if (this._currentTime <= 0)
                {
                    //Debug.Log("shoot");
                    this._bulletSystem.ShootBullet(_weaponComponent);
                    this._currentTime += this.timeBetweenShots;
                }
            }
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
    }
}