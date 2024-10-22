using UI;
using Zenject;

namespace EventBus
{
    public sealed class CheckIfDeadEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly UIService _uiService;
        private readonly VisualPipeline _visualPipeline;
        public CheckIfDeadEventHandler(EventBus eventBus, UIService uiService, VisualPipeline visualPipeline)
        {
            _eventBus = eventBus;
            _uiService = uiService;
            _visualPipeline = visualPipeline;
        }

        private void HandleEvent(CheckIfDeadEvent evt)
        {
            if(evt.Target == null || evt.Target.HealthComponent.CurrentHealth > 0) return;
            
            evt.Target.SetIsDead();

            HeroView targetView = evt.Target.View;
            CrossDeadHeroTask crossDeadHeroTask = new CrossDeadHeroTask(_uiService, targetView);
            _visualPipeline.AddGameTask(crossDeadHeroTask);
        }
        public void Initialize()
        {
            _eventBus.SubscribeHandler<CheckIfDeadEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<CheckIfDeadEvent>(HandleEvent);
        }
    }
}