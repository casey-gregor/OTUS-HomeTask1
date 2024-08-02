using System;
using System.Collections;
using UnityEngine;

namespace Assets.Projects.ZombieShooter.Scripts
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