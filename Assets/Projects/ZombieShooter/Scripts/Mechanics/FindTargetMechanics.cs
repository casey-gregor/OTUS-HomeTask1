using Atomic.Elements;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class FindTargetMechanics
    {
        IAtomicVariable<Vector3> _target;
        IAtomicAction<Vector3> _moveDirection;

        public FindTargetMechanics(IAtomicValue<GameObject> target, IAtomicAction<Vector3> moveDirection)
        {
            _target.Value = target.Value.transform.position;
            _moveDirection = moveDirection;

        }


       
    }
}