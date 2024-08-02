using Atomic.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class SetAnimationBoolMechanics
    {
        private Animator _animator;
        private int _hash;

        public SetAnimationBoolMechanics(Animator animator, int hash, IAtomicObservable<bool> value)
        {
            _animator = animator;
            _hash = hash;

            value.Subscribe(SetBool);
        }

        private void SetBool(bool value)
        {
            _animator.SetBool(_hash, value);
        }
    }
}