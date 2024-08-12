using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using UnityEngine;

namespace ZombieShooter
{
    public class Bullet : AtomicEntity
    {
        [Get(APIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _moveComponent.MoveDirection;

        [Get(APIKeys.DAMAGE)]
        public IAtomicVariable<int> Damage => _damage;

        [Get(APIKeys.IS_ACTIVE)]
        public IAtomicVariable<bool> IsActive => _isActive;

        [HideInInspector] public Action<bool> InactiveHandler = null;
        
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private AtomicVariable<int> _damage;
        private AtomicVariable<bool> _isActive = new AtomicVariable<bool> { Value = true };

        private InBoundsCheckMechanics _inBoundsCheckMechanics;


        private void Awake()
        {
            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return transform.position;
            });

            LevelBounds levelBounds = FindObjectOfType<LevelBounds>();

            _inBoundsCheckMechanics = new InBoundsCheckMechanics(rootPosition, _isActive, levelBounds);

            
        }

        private void Update()
        {

            _moveComponent.OnUpdate(Time.deltaTime);
            _inBoundsCheckMechanics.OnUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicEntity entity))
            {
                entity.GetAction<int>(APIKeys.DEDUCT_HITPOINTS).Invoke(_damage.Value);

                _isActive.Value = false;
            }
        }

    }

}
