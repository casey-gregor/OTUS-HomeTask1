using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class Zombie : AtomicObject
    {
        [Get(APIKeys.TARGET)]
        public AtomicVariable<AtomicObject> Target => _targetObject;

        [Get(APIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _moveComponent.MoveDirection;

        [Get(APIKeys.IS_DEAD)]
        public AtomicVariable<bool> IsDead => _lifeComponent.isDead;

        [Get(APIKeys.DAMAGE)]
        public AtomicVariable<int> Damage => _damageAmount;

        [Get(APIKeys.DAMAGE_INTERVAL)]
        public AtomicVariable<float> DamageInterval => _damageInterval;

        [Get(APIKeys.HIT_POINTS)]
        public IAtomicVariable<int> HitPoints => _lifeComponent._hitPoints;

        [Get(APIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => _lifeComponent.DeductHitPointEvent;


        [HideInInspector] public AtomicVariable<IAtomicEntity> _target;

        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;

        [SerializeField] private AtomicVariable<AtomicObject> _targetObject;
        [SerializeField] private AtomicVariable<int> _damageAmount;
        [SerializeField] private AtomicVariable<float> _damageInterval;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private MeleeAttackMechanics _attackMechanics;


        public void Awake()
        {

            _moveComponent.AddCondition(_lifeComponent.IsAlive);
            
            _rotationComponent.Construct();
            _rotationComponent.AddCondition(_lifeComponent.IsAlive);

            _lifeComponent.Construct();


            var targetEntity = new AtomicFunction<AtomicEntity>(() =>
            {
                return _targetObject.Value.GetComponent<AtomicEntity>(); ;
            });

            var targetPosition = new AtomicFunction<Vector3>(() =>
            {
                return _targetObject.Value.transform.position;

            });

            var hasTarget = new AtomicFunction<bool>(() =>
            {
                return _targetObject.Value != null;
            });

            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return transform.position;
            });


            _lookAtTargetMechanics = new LookAtTargetMechanics(
                targetPosition, 
                rootPosition, 
                hasTarget, 
                _rotationComponent.RotateDirection,
                _moveComponent.MoveDirection);

            _attackMechanics = new MeleeAttackMechanics(
                _targetObject, 
                rootPosition, 
                _moveComponent.CanMove,
                _damageInterval,
                _damageAmount);


            AddLogic(_lookAtTargetMechanics);
            AddLogic(_moveComponent);
            AddLogic(_attackMechanics);
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            OnFixedUpdate(fixedDeltaTime);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            OnUpdate(deltaTime);

        }

    }
}