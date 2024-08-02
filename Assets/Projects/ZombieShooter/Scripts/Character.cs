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

        [Get(APIKeys.MoveDirection)]
        public IAtomicVariable<Vector3> MoveDirection => _core.MoveComponent.MoveDirection;

        [Get(APIKeys.RotateDirection)]
        public IAtomicVariable<Vector3> RotateDirection => _core.RotationComponent.RotateDirection;

        [Get(APIKeys.ShootAction)]
        public IAtomicAction ShootAction => _core.ShootComponent.ShootActionEvent;

        [Get(APIKeys.ShootRequest)]
        public IAtomicAction ShootRequest => _core.ShootComponent.ShootRequestEvent;

        [Get(APIKeys.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => _core.LifeComponent.TakeDamageEvent;

        private void Awake()
        {
            _core.Construct(this);
            _animation.Construct(_core);
        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;

            OnUpdate(deltaTime);
        }
    }
}

