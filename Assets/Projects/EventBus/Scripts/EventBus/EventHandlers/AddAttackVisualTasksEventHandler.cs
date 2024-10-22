using Zenject;

namespace EventBus
{
    public sealed class AddAttackVisualTasksEventHandler : IInitializable, ILateDisposable
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly EventBus _eventBus;

        public AddAttackVisualTasksEventHandler(VisualPipeline visualPipeline, EventBus eventBus)
        {
            _visualPipeline = visualPipeline;
            _eventBus = eventBus;
        }

        private void HandleEvent(AddAttackVisualTasksEvent evt)
        {
            _visualPipeline.AddGameTask(new AttackVisualTask(evt.Attacker, evt.Target));
            
            // _visualPipeline.AddGameTask(new SetStatsTask(evt.Attacker, evt.Target));
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<AddAttackVisualTasksEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<AddAttackVisualTasksEvent>(HandleEvent);
        }
    }
}