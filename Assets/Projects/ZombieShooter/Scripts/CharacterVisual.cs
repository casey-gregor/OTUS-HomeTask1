using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterVisual
    {
        [SerializeField] private Animator _animator;

        private int _isMovingHash = Animator.StringToHash("IsMoving");
        public void Construct(CharacterCore core)
        {
            core.MoveComponent.MoveDirection.Subscribe(HandleDirectionEvent);
        }

        private void HandleDirectionEvent(Vector3 vector)
        {
            Debug.Log("vector : " + vector);

            bool isMoving = vector.sqrMagnitude > 0;
            _animator.SetBool(_isMovingHash, isMoving);
        }
    }
}