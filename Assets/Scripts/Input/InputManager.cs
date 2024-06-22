using System;
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameUpdateListener
    {
        public event Action OnSpacePressedEvent;
        public int HorizontalDirection { get; private set; }
        //private void Start()
        //{
        //    IGameListener.Register(this);
        //}

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpacePressedEvent?.Invoke();
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