using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace ZombieShooter
{
    public class ZombieHandsWeapon : MonoBehaviour
    {
        [SerializeField] private AtomicObject _zombie;

        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent(out IAtomicEntity entity))
            {
                _zombie.GetVariable<IAtomicEntity>(APIKeys.TARGET).Value = entity;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //if (other.TryGetComponent(out IAtomicEntity entity) && CanDamage(entity))
            //{

            //    TryDamage(entity, _zombie);
            //}
        }

        //private bool CanDamage(IAtomicEntity entity)
        //{
        //    if(entity.TryGetVariable<LayerMask>(APIKeys.LAYER, out IAtomicVariable<LayerMask> entityLayer))
        //    {
        //        return entityLayer.Value != _zombie.GetVariable<LayerMask>(APIKeys.LAYER).Value;
        //    }
        //    return false;
        //}

        //private void TryDamage(IAtomicEntity target, AtomicEntity damager)
        //{
        //    if (target.TryGetAction<IAtomicEntity, AtomicEntity>
        //                (APIKeys.TRY_TAKE_DAMAGE_ACTION, out IAtomicAction<IAtomicEntity, AtomicEntity> tryTakeDamage))
        //    {
        //        tryTakeDamage.Invoke(target, damager);
        //    }
        //}
        
    }
}