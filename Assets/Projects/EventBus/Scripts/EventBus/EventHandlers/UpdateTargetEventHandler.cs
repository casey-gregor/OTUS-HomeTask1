using Zenject;

namespace EventBus
{
    public class UpdateTargetEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly PipelineContext _context;
        public UpdateTargetEventHandler(EventBus eventBus, PipelineContext context)
        {
            _eventBus = eventBus;
            _context = context;
        }
        
        private void HandleEvent(UpdateTargetEvent evt)
        {
            _context.SetTargetHeroEntity(evt.Target);
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<UpdateTargetEvent>(HandleEvent);
        }


        public void LateDispose()
        {
            _eventBus.SubscribeHandler<UpdateTargetEvent>(HandleEvent);
        }
    }
}