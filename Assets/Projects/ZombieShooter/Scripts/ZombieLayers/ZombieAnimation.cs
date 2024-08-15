using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class ZombieAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        private int _isMovingHash = Animator.StringToHash("IsMoving");
        private int _isDeadHash = Animator.StringToHash("isDead");
        private int _attackHash = Animator.StringToHash("Attack");

        private ZombieCore _core;

        private bool canSetAttackTrigger = true;

        private SetAnimationBoolMechanics _moveAnimationMechanics;
        private SetAnimationBoolMechanics _deadAnimationMechanics;
        public void Construct(ZombieCore core)
        {
            _core = core;

            _moveAnimationMechanics = new SetAnimationBoolMechanics(_animator, _isMovingHash, _core.MoveComponent.IsMoving);
            _deadAnimationMechanics = new SetAnimationBoolMechanics(_animator, _isDeadHash, _core.LifeComponent.IsDead);

            _core.AttackComponent.AttackRequestEvent.Subscribe(HandleAttackRequest);

            _animatorDispatcher.AnimationEvent += HandleAttackEvent;
            _animatorDispatcher.AnimationEvent += HandleDeathEvent;

        }

        private void HandleDeathEvent(string value)
        {
            if(value == "Dead")
                _core.DeadEvent.Invoke(_core.Zombie);
        }

        private void HandleAttackRequest()
        {
            if (canSetAttackTrigger)
            {
                _animator.SetTrigger(_attackHash);
                canSetAttackTrigger = false;
            }
            
        }

        private void HandleAttackEvent(string value)
        {
            if(value == "Attack")
            {
                _core.AttackComponent.AttackActionEvent.Invoke();
                canSetAttackTrigger = true;
            }
        }


    }
}