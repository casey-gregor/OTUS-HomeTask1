using UI;
using Zenject;

namespace EventBus
{
    public sealed class CheckIfDeadEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly UIService _uiService;
        public CheckIfDeadEventHandler(EventBus eventBus, UIService uiService)
        {
            _eventBus = eventBus;
            _uiService = uiService;
        }

        private void HandleEvent(CheckIfDeadEvent evt)
        {
            if(evt.Target == null || evt.Target.HealthComponent.CurrentHealth > 0) return;
            
            evt.Target.HealthComponent.SetIsDead();

            CrossDeadHeroTask crossDeadHeroTask = new CrossDeadHeroTask(_uiService, evt.Target.View);
            _eventBus.RaiseEvent(new AddVisualTaskEvent(crossDeadHeroTask));
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