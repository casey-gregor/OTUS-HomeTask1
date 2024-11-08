using System;
using Atomic.AI;
using Game;
using Game.Engine;
using UnityEngine;

namespace Projects.BehaviourTree.Scripts
{
    [Serializable]
    public class MoveResourcesToBarnBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            var character = blackboard.GetCharacter();
            var characterStorage = character.GetComponent<ResourceStorageComponent>();

            var barn = blackboard.GetBarn();
            var barnStorage = barn.GetComponent<ResourceStorageComponent>();

            return MoveResources(characterStorage, barnStorage);
        }

        private BTResult MoveResources(
            ResourceStorageComponent fromStorage, 
            ResourceStorageComponent toStorage)
        {
            
            if (toStorage.FreeSlots == 0)
            {
                return BTResult.FAILURE;
            }
            
            int resourcesToAdd = Math.Min(fromStorage.Current, toStorage.FreeSlots);
            fromStorage.RemoveResources(resourcesToAdd);
            toStorage.AddResources(resourcesToAdd);
            
            return BTResult.SUCCESS;
        }
    }
}