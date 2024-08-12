using Assets.Scripts.ZombieShooterScripts.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class Character : AtomicObject
    {
        [SerializeField] private CharacterCore _core;
        [SerializeField] private CharacterAnimation _animation;
        [SerializeField] private CharacterVFX _vfx;
        [SerializeField] private CharacterSFX _sfx;

        [Get(APIKeys.MOVE_DIRECTION)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.MoveDirection;

        [Get(APIKeys.ROTATE_DIRECTION)]
        public IAtomicVariable<Vector3> RotateDirection => _core.RotationComponent.RotateDirection;

        [Get(APIKeys.SHOOT_REQUEST)]
        public IAtomicAction ShootRequest => _core.ShootComponent.ShootRequestEvent;

        [Get(APIKeys.DEDUCT_HITPOINTS)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.DeductHitPointEvent;

        [Get(APIKeys.IS_DEAD)]
        public IAtomicValue<bool> IsDead => _core.LifeComponent.isDead;

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

