using System;
using UnityEngine;

namespace ZombieShooter
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        public event Action<string> AnimationEvent;

        public void EventReceived(string value)
        {
            AnimationEvent?.Invoke(value);
        }
    }
}