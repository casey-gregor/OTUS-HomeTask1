using Assets.Scripts.ZombieShooterScripts.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class Zombie : AtomicObject, IDamagable
    {
        [Get(APIKeys.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => _lifeComponent.TakeDamageEvent;

        [Get(APIKeys.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => _moveComponent.MoveDirection;
        public RotationComponent RotationComponent => _rotationComponent;
        public LifeComponent LifeComponent => _lifeComponent;

        [SerializeField] private RotationComponent _rotationComponent;
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private MoveComponent _moveComponent;

        [SerializeField] private AtomicVariable<float> _radius;
        [SerializeField] private AtomicVariable<LayerMask> _layerMask;
        [SerializeField] private AtomicVariable<GameObject> _targetObject;

        private LookAtTargetMechanics _lookAtTargetMechanics;
        private FindClosestTargetMechanics _findClosestTargetMechanics;

        private void Awake()
        {
            _moveComponent.AddCondition(_lifeComponent.IsAlive);
            
            _rotationComponent.Construct();
            _rotationComponent.AddCondition(_lifeComponent.IsAlive);

            _lifeComponent.Construct();

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

            _findClosestTargetMechanics = new FindClosestTargetMechanics(_targetObject, rootPosition, _radius, _layerMask);
            _lookAtTargetMechanics = new LookAtTargetMechanics(
                targetPosition, 
                rootPosition, 
                hasTarget, 
                _rotationComponent._rotationEvent,
                _moveComponent.DirectionEvent);

            AddLogic(_findClosestTargetMechanics);
            AddLogic(_lookAtTargetMechanics);
            AddLogic(_moveComponent);
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

        public void TakeDamage(int damage)
        {
            _lifeComponent.TakeDamage(damage);
        }
    }
}