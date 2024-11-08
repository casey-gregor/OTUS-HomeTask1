using Atomic.AI;
using Game.Engine;
using UnityEngine;

namespace Engine
{
    public class IsStorageFullCondition : IBlackboardCondition
    {
        [SerializeField, BlackboardKey]
        private int storageObject;

        public bool Invoke(IBlackboard blackboard)
        {
            var objectWithStorage = blackboard.GetObject<GameObject>(storageObject);
            var storage = objectWithStorage.GetComponent<ResourceStorageComponent>();
            return storage.IsFull();
        }
    }
}