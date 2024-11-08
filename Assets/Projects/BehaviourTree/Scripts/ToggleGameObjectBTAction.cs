using System;
using Atomic.AI;
using UnityEngine;

namespace Projects.BehaviourTree.Scripts
{
    [Serializable]
    public class ToggleGameObjectBTAction : IBlackboardAction
    {
        [SerializeField, BlackboardKey]
        private int targetKey;
        [SerializeField]
        private bool enable;
        public void Invoke(IBlackboard blackboard)
        {
            blackboard.GetObject<GameObject>(targetKey).SetActive(enable);
        }
    }
}