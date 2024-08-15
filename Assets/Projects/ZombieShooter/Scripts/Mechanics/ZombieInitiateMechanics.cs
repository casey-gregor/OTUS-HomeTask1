using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

namespace ZombieShooter
{
    public class ZombieInitiateMechanics
    {
        private IAtomicValue<Zombie> _zombie;
        private AtomicObject _target;
        private IAtomicAction<Zombie> _enqueueAction;
        public ZombieInitiateMechanics(
            IAtomicValue<Zombie> zombie, 
            AtomicObject target,
            AtomicEvent initiateEvent,
            IAtomicAction<Zombie> enqueueAction)
        {
            _zombie = zombie;
            _target = target;
            _enqueueAction = enqueueAction;

            initiateEvent.Subscribe(InitiateZombie);
        }

        public void InitiateZombie()
        {
            Zombie zombie = _zombie.Value;

            if (zombie.GetVariable<bool>(ZombieAPIKeys.IS_DEAD).Value)
            {
                zombie.GetVariable<bool>(ZombieAPIKeys.IS_DEAD).Value = false;
                zombie.GetVariable<int>(ZombieAPIKeys.HIT_POINTS).Value = 1;
            }

            zombie.GetVariable<AtomicObject>(ZombieAPIKeys.TARGET).Value = _target;

            zombie.ListenEvent<Zombie>(ZombieAPIKeys.DEAD_EVENT, RemoveZombie);


        }

        private void RemoveZombie(Zombie zombie)
        {
            zombie.UnlistenEvent<Zombie>(ZombieAPIKeys.DEAD_EVENT, RemoveZombie);
            _enqueueAction.Invoke(zombie);
        }
    }
}