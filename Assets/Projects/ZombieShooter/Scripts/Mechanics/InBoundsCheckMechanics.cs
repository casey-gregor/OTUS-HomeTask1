using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public class InBoundsCheckMechanics
    {
        private IAtomicValue<Vector3> _bulletPosition;
        private AtomicVariable<bool> _isDead;
        private LevelBounds _levelBounds;

        public InBoundsCheckMechanics(IAtomicValue<Vector3> bulletPosition, AtomicVariable<bool> isDead, LevelBounds levelBounds)
        {
            _bulletPosition = bulletPosition;
            _isDead = isDead;
            _levelBounds = levelBounds;
        }

        public void OnUpdate()
        {
            CheckIfInBounds();
        }

        private void CheckIfInBounds()
        {
            if (!InBounds(_bulletPosition.Value))
            {
                _isDead.Value = true;
            }
        }

        private bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.z;
            return positionX > _levelBounds.leftBound.position.x
                   && positionX < _levelBounds.rightBound.position.x
                   && positionY > _levelBounds.bottomBound.position.z
                   && positionY < _levelBounds.topBound.position.z;
        }
    }
}