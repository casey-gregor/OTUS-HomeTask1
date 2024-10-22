using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class HealEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;
        public HealEventHandler(EventBus eventBus, VisualPipeline visualPipeline)
        {
            _eventBus = eventBus;
            _visualPipeline = visualPipeline;
        }
        
        private void HandleEvent(HealEvent evt)
        {
            Debug.Log("in HealEventHandler");
            Debug.Log("evt.Target : " + evt.Target.View.name);
            Debug.Log("evt.Target health : " + evt.Target.HealthComponent.CurrentHealth);
            evt.Target.AddHealth(evt.HealAmount);
            Debug.Log("evt.Target health after add : " + evt.Target.HealthComponent.CurrentHealth);
            SetStatsTask setTargetStatsTask = new SetStatsTask(evt.Attacker, evt.Target);
            _visualPipeline.AddGameTask(setTargetStatsTask);
 
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