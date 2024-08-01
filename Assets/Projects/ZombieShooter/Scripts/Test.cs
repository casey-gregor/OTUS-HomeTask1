using Atomic.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class Test
    {
        public AtomicEvent TestEvent;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public void OnUpdate()
        {
            TestEvent?.Invoke();
        }
    }
}