using Atomic.Elements;
using Atomic.Objects;
using Atomic.Extensions;

namespace ZombieShooter
{
    public class ZombieInitiateMechanics
    {

        public void InitiateZombie(Zombie _zombie, AtomicObject _target, AtomicEvent<Zombie> EnqueueAction)
        {
           
            if (_zombie.GetVariable<bool>(ZombieAPIKeys.IS_DEAD).Value)
            {
                _zombie.GetVariable<bool>(ZombieAPIKeys.IS_DEAD).Value = false;
                _zombie.GetVariable<int>(ZombieAPIKeys.HIT_POINTS).Value = 1;
            }

            _zombie.GetVariable<AtomicObject>(ZombieAPIKeys.TARGET).Value = _target;

            IAtomicObservable<bool> isDeadObservable = _zombie.GetObservable<bool>(ZombieAPIKeys.IS_DEAD);

            isDeadObservable.Subscribe(_zombie._core.isDeadHandler = value =>
            {
                isDeadObservable.Unsubscribe(_zombie._core.isDeadHandler);
                if(value)
                    EnqueueAction.Invoke(_zombie);
            });
            

        }

       
    }
}