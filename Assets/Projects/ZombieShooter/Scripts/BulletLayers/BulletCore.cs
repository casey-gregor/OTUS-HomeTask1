using Atomic.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class BulletCore
    {
        [HideInInspector] public Action<bool> InactiveHandler = null;

        public MoveComponent _moveComponent;
        public AtomicVariable<int> _damage;
        public AtomicVariable<bool> _isDead;
        public InBoundsCheckMechanics _inBoundsCheckMechanics;

        public void Construct(Bullet bullet)
        {
            var rootPosition = new AtomicFunction<Vector3>(() =>
            {
                return bullet.transform.position;
            });

            _inBoundsCheckMechanics = new InBoundsCheckMechanics(rootPosition, _isDead, bullet._levelBounds.Value);

        }
    }
}