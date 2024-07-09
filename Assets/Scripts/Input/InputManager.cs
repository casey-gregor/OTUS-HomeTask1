using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : IGameUpdateListener
    {
        public event Action OnSpacePressedEvent;
        public int HorizontalDirection { get; private set; }

        public InputManager()
        {
            IGameListener.Register(this);
        }
        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.OnSpacePressedEvent?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.HorizontalDirection = 1;
            }
            else
            {
                this.HorizontalDirection = 0;
            }
        }
    }
}