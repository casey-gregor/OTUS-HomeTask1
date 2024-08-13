using Atomic.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class CompositeCondition
    {
        private HashSet<Func<bool>> _conditions = new HashSet<Func<bool>>();
        private HashSet<AtomicFunction<bool>> _atomicConditions = new HashSet<AtomicFunction<bool>>();

        public bool IsTrue()
        {

            foreach (var condition in _conditions)
            {
                if (condition() == false)
                    return false;
            }

            foreach (var condition in _atomicConditions)
            {
                if(condition.Value == false)
                    return false;
            }

            return true;
        }

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public void AddCondition(AtomicFunction<bool> condition)
        {
            _atomicConditions.Add(condition);
        }
    }
}