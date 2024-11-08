using System;
using Atomic.AI;
using Game;
using Game.Engine;
using UnityEngine;

namespace Projects.BehaviourTree
{
    [Serializable]
    public class CanFindClosestTreeCondition : IBlackboardCondition
    {

        public bool Invoke(IBlackboard blackboard)
        {
            if (!blackboard.TryGetTreeService(out TreeService treeService))
            {
                return false;
            }
            GameObject character = blackboard.GetCharacter();
            if (treeService.FindClosest(character.transform.position, out GameObject closestTree))
            {
                blackboard.SetTarget(closestTree);
                return true;
            }

            blackboard.DelTarget();
            return false;
        }
    }
}