using Zenject;

namespace EventBus
{
    public sealed class HealEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        public HealEventHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void HandleEvent(HealEvent evt)
        {
            evt.Target.HealthComponent.AddHealth(evt.HealAmount);
            SetStatsTask setStatsTask = new SetStatsTask(evt.Attacker, evt.Target);
            _eventBus.RaiseEvent(new AddVisualTaskEvent(setStatsTask));
 
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<HealEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<HealEvent>(HandleEvent);
        }
    }
}