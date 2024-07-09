using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class TimerService : IGameFixedUpdateListener
    {
        public bool timerRunning { get; private set; }

        private float timer;
        private float elapsedTime;
        private GameObject obj;

        private Action handlerFuncSimple;
        private Action<GameObject> handlerFuncObject;
        public TimerService()
        {
            IGameListener.Register(this);
        }

        public void Set(GameObject objSet, float timer, Action<GameObject> handlerFunc)
        {
            this.obj = objSet;
            this.timer = timer;
            this.elapsedTime = 0;
            this.timerRunning = true;
            this.handlerFuncObject = handlerFunc;
        }

        public void Set(float timer, Action handlerFunc)
        {
            this.timer = timer;
            this.elapsedTime = 0;
            this.timerRunning = true;
            this.handlerFuncSimple = handlerFunc;
        }

        public void Resume()
        {
            this.timerRunning = true;
        }

        public void Stop()
        {
            this.timerRunning = false;
        }
  
        public void OnFixedUpdate()
        {
            if (this.timerRunning)
            {

                this.elapsedTime += Time.fixedDeltaTime;
                if (this.elapsedTime >= this.timer)
                {
                    Stop();

                    this.handlerFuncObject?.Invoke(obj);
                    this.handlerFuncSimple?.Invoke();
                }
            }
        }
    }

}
