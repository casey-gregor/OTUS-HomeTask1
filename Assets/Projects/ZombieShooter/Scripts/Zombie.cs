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
        [Get(APIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => _lifeComponent.DeductHitPointEvent;

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

        [Get(APIKeys.TARGET)]
        public AtomicVariable<AtomicObject> Target => _targetObject;

        [Get(APIKeys.INITIATE)]
        public IAtomicAction<AtomicVariable<AtomicObject>> Initiate => InitiateEvent;


        public AtomicEvent<AtomicVariable<AtomicObject>> InitiateEvent;

        [HideInInspector] public AtomicVariable<IAtomicEntity> _target;

        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private LifeComponent _lifeComponent;

        [SerializeField] private AtomicVariable<AtomicObject> _targetObject;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private MeleeAttackMechanics _attackMechanics;

        [SerializeField] private AtomicVariable<int> _damageAmount;
        [SerializeField] private AtomicVariable<float> _damageInterval;

        public void Awake()
        {
            InitiateEvent.Subscribe(InitiateZombie);

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

        public void InitiateZombie(AtomicVariable<AtomicObject> target)
        {
            _targetObject.Value = target.Value;

            target.Subscribe((value) => 
            { 
                _targetObject.Value = value;
            });

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