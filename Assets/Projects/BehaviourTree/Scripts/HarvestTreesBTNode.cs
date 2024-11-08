using System;
using Atomic.AI;
using Game;
using Game.Engine;
using UnityEngine;

namespace Projects.BehaviourTree.Scripts
{
    [Serializable]
    public class HarvestTreesBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            var character = blackboard.GetCharacter();
            if(character.TryGetComponent<HarvestComponent>(out HarvestComponent harvestComponent))
            {
                if (!character.TryGetComponent<ResourceStorageComponent>(out ResourceStorageComponent characterStorage))
                {
                    return BTResult.FAILURE;
                }

                GameObject tree = blackboard.GetTarget();
                
                if(characterStorage.IsNotFull() && tree.activeInHierarchy)
                {
                    harvestComponent.StartHarvest();
                    return BTResult.RUNNING;
                }

                if (characterStorage.IsNotFull() && !tree.activeInHierarchy)
                {
                    return BTResult.SUCCESS;
                }

                if (characterStorage.IsFull())
                {
                    return BTResult.SUCCESS;
                }
            }
            return BTResult.FAILURE;
        }
    }
}