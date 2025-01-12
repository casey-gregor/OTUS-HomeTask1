using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class Character : AtomicObject
    {
        [SerializeField] private CharacterCore _core;
        [SerializeField] private CharacterAnimation _animation;
        [SerializeField] private CharacterVFX _vfx;
        [SerializeField] private CharacterSFX _sfx;

        [Get(CharacterAPIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.MoveDirection;

        [Get(CharacterAPIKeys.ROTATE_DIRECTION)]
        public IAtomicVariable<Vector3> RotateDirection => _core.RotationComponent.RotateDirection;

        [Get(CharacterAPIKeys.SHOOT_REQUEST)]
        public IAtomicAction ShootRequest => _core.ShootComponent.shootRequestEvent;

        [Get(CharacterAPIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.DeductHitPointEvent;

        [Get(CharacterAPIKeys.IS_DEAD)]
        public IAtomicValue<bool> IsDead => _core.LifeComponent.IsDead;

        [Get(CharacterAPIKeys.BULLET_COUNT)] 
        public IAtomicValue<int> BulletCount => _core.ShootComponent.bulletsInMagazine;
        
        [Get(CharacterAPIKeys.ADD_BULLET)] 
        public IAtomicAction AddBullet => _core.ShootComponent.bulletReloaded;

        [Get(CharacterAPIKeys.REMOVE_BULLET)] 
        public IAtomicAction RemoveBullet => _core.ShootComponent.bulletShot;
        

        private void Awake()
        {
            _core.Construct(this);
            _animation.Construct(_core);
            _vfx.Construct(_core);
            _sfx.Construct(_core);

        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;

            OnUpdate(deltaTime);
        }
    }
}

