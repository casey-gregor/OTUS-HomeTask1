using Assets.Projects.ZombieShooter.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        private int _isMovingHash = Animator.StringToHash("IsMoving");
        private int _isShootingHash = Animator.StringToHash("Shoot");
        private int _isDeadHash = Animator.StringToHash("isDead");

        private bool canSetShootTrigger = true;

        private CharacterCore _core;

        private SetAnimationBoolMechanics _moveAnimationMechanics;
        private SetAnimationBoolMechanics _deadAnimationMechanics;
        public void Construct(CharacterCore core)
        {
            _core = core;

            _moveAnimationMechanics = new SetAnimationBoolMechanics(_animator, _isMovingHash, _core.MoveComponent.IsMoving);
            _deadAnimationMechanics = new SetAnimationBoolMechanics(_animator, _isDeadHash, _core.LifeComponent.isDead);

            _core.ShootComponent.ShootActionEvent.Subscribe(HandleShootActionEvent);
            _animatorDispatcher.AnimationEvent += HandleShootAction;

        }


        private void HandleShootAction(string value)
        {
            if(value == "Shoot")
            {
                _core.ShootComponent.FireEvent?.Invoke();
                canSetShootTrigger = true;
            }
        }

        private void HandleShootActionEvent()
        {
            if(canSetShootTrigger)
            {
                _animator.SetTrigger(_isShootingHash);
                canSetShootTrigger = false;
            }
        }

    }
}