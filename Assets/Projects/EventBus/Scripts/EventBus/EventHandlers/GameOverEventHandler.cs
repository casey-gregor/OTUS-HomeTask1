using UI;
using Zenject;

namespace EventBus
{
    public sealed class GameOverEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;
        private readonly UIService _uiService;

        public GameOverEventHandler(
            EventBus eventBus,
            VisualPipeline visualPipeline, 
            UIService uiService)
        {
            _eventBus = eventBus;
            _visualPipeline = visualPipeline;
            _uiService = uiService;
        }

        private void HandleEvent(GameOverEvent evt)
        {
            _visualPipeline.AddGameTask(new GameOverTask(_uiService.GetGameOverPanel(), evt.Winner));
        }
        public void Initialize()
        {
            _eventBus.SubscribeHandler<GameOverEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<GameOverEvent>(HandleEvent);
        }
    }
}