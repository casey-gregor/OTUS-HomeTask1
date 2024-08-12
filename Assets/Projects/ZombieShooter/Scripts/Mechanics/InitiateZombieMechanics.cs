using Atomic.Elements;
using Atomic.Objects;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using ZombieShooter;
using Atomic.Extensions;
using System;

namespace ZombieShooter
{
    public class InitiateZombieMechanics
    {

        private Action<bool> isDeadHandler = null;

        public void InitiateZombie(Zombie _zombie, AtomicObject _target, AtomicEvent<Zombie> EnqueueAction)
        {
           
            if (_zombie.GetVariable<bool>(APIKeys.IS_DEAD).Value)
            {
                _zombie.GetVariable<bool>(APIKeys.IS_DEAD).Value = false;
                _zombie.GetVariable<int>(APIKeys.HIT_POINTS).Value = 1;
            }

            _zombie.GetVariable<AtomicObject>(APIKeys.TARGET).Value = _target;

            IAtomicObservable<bool> isDeadObservable = _zombie.GetObservable<bool>(APIKeys.IS_DEAD);

            isDeadObservable.Subscribe(isDeadHandler = value =>
            {
                isDeadObservable.Unsubscribe(isDeadHandler);
                if(value)
                    EnqueueAction.Invoke(_zombie);
            });
            

        }

       
    }
}