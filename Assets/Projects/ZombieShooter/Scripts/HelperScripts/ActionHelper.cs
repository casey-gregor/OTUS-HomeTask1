using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace ZombieShooter
{
    public class ActionHelper : MonoBehaviour
    {
        [SerializeField] private AtomicObject _character;
        public Zombie _zombie;
        [SerializeField] private ZombieSpawnController _spawnController;

        public int damage;

        [SerializeField] private AtomicEntity _bullet;

        public void DamageCharacter(int damage)
        {
            _character.GetAction<int>(CharacterAPIKeys.DEDUCT_HITPOINTS).Invoke(damage);
        }

        public void KillZombie(Zombie zombie)
        {
            //_spawnController.EnqueueZombie(zombie);

            if (zombie.TryGetAction<IAtomicEntity, AtomicEntity>
                    (ZombieAPIKeys.DEDUCT_HITPOINTS, out IAtomicAction<IAtomicEntity, AtomicEntity> tryTakeDamage))
            {
                tryTakeDamage.Invoke(zombie, _bullet);
            }
        }
       
    }
}