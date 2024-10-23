using UnityEngine;

namespace EventBus
{
    public struct AddVisualTaskEvent
    {
        public GameTask Task  { get; set; }
        public AddVisualTaskEvent(GameTask task)
        {
            Task = task;
        }
    }
}