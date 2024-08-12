using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
            _character.GetAction<int>(APIKeys.DEDUCT_HITPOINTS).Invoke(damage);
        }

        public void KillZombie(Zombie zombie)
        {
            //_spawnController.EnqueueZombie(zombie);

            if (zombie.TryGetAction<IAtomicEntity, AtomicEntity>
                    (APIKeys.TRY_TAKE_DAMAGE_ACTION, out IAtomicAction<IAtomicEntity, AtomicEntity> tryTakeDamage))
            {
                tryTakeDamage.Invoke(zombie, _bullet);
            }
        }
       
    }
}