using Zenject;

namespace EventBus
{
    public sealed class CheckTargetEffectsEventHandler : IInitializable, ILateDisposable
    {
        private PipelineContext pipelineContext;
        private EventBus _eventBus;

        public CheckTargetEffectsEventHandler(PipelineContext pipelineContext, EventBus eventBus)
        {
            this.pipelineContext = pipelineContext;
            _eventBus = eventBus;
        }

        private void HandleEvent(CheckTargetEffectsEvent evt)
        {
            if(evt.Target.TryGetEffect(out IEffect enemyEffect) && enemyEffect.Type == evt.Type)
            {
                enemyEffect.Source = evt.Target;
                enemyEffect.Target = evt.Attacker;
                _eventBus.RaiseEvent(enemyEffect); 
            }
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<CheckTargetEffectsEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<CheckTargetEffectsEvent>(HandleEvent);
        }
    }
    
}