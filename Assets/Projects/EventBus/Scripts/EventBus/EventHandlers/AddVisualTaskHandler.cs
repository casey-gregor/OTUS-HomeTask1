using UnityEngine;
using Zenject;

namespace EventBus
{
    public class AddVisualTaskHandler: IInitializable, ILateDisposable
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly EventBus _eventBus;

        public AddVisualTaskHandler(VisualPipeline visualPipeline, EventBus eventBus)
        {
            _visualPipeline = visualPipeline;
            _eventBus = eventBus;
        }

        private void HandleEvent(AddVisualTaskEvent evt)
        {
            Debug.Log("In AddVisualTaskEvent handler");
            _visualPipeline.AddGameTask(evt.Task);
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<AddVisualTaskEvent>(HandleEvent);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<AddVisualTaskEvent>(HandleEvent);
        }
    }
}