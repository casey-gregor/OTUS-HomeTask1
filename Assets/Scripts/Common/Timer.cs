using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Timer : IGameFixedUpdateListener
    {
        public bool TimerRunning { get; private set; }
        public float ElapsedTime { get; private set; }

        private float timer;
        private float elapsedTime;
        private GameObject obj;

        private event Action handlerFuncSimple;
        private event Action<GameObject> handlerFuncObject;

        public void Set(GameObject objSet, float timer, Action<GameObject> handlerFunc)
        {
            this.obj = objSet;
            this.timer = timer;
            this.elapsedTime = 0;
            this.TimerRunning = true;
            this.handlerFuncObject = handlerFunc;
        }

        public void Set(float timer, Action handlerFunc)
        {
            this.timer = timer;
            this.elapsedTime = 0;
            this.TimerRunning = true;
            this.handlerFuncSimple = handlerFunc;
            
        }

        public void Resume()
        {
            this.TimerRunning = true;
        }

        public void Stop()
        {
            this.TimerRunning = false;
        }

        private void ExecuteTimer()
        {
            if (this.TimerRunning)
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
  
        public void OnFixedUpdate()
        {
            ExecuteTimer();
        }
    }

}
