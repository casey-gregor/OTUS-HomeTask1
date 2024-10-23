using UI;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class GameOverEventHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly UIService _uiService;

        public GameOverEventHandler(
            EventBus eventBus,
            UIService uiService)
        {
            _eventBus = eventBus;
            _uiService = uiService;
        }

        private void HandleEvent(GameOverEvent evt)
        {
            if (_uiService.TryGetGameOverPanel(out GameObject gameOverPanel))
            {
                GameOverVisualTask gameOverVisualTask = new GameOverVisualTask(gameOverPanel, evt.Winner);
                _eventBus.RaiseEvent(new AddVisualTaskEvent(gameOverVisualTask));
            }
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