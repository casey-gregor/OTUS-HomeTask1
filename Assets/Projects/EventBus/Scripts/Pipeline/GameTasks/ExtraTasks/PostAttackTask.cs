using UnityEngine;

namespace EventBus
{
    public sealed class PostAttackTask<T> : GameTask
    {
        private readonly EventBus _eventBus;
        private readonly T _customEvent;

        public PostAttackTask(EventBus eventBus, T customEvent)
        {
            _eventBus = eventBus;
            _customEvent = customEvent;
        }

        protected override void OnRun()
        {
            Debug.Log("in Post attack task");
            _eventBus.RaiseEvent(_customEvent);
            Finish();
        }
    }
}