using System;
using UnityEngine;
using Zenject;

public class TimerService : IGameFixedUpdateListener
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
        elapsedTime = 0;
        timerRunning = true;
        this.handlerFuncObject = handlerFunc;
    }

    public void Set(float timer, Action handlerFunc)
    {
        this.timer = timer;
        elapsedTime = 0;
        timerRunning = true;
        this.handlerFuncSimple = handlerFunc;
    }

    public void Resume()
    {
        timerRunning = true;
    }

    public void Stop()
    {
        timerRunning = false;
    }
  
    public void OnFixedUpdate()
    {
        if (timerRunning)
        {

            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= timer)
            {
                Stop();

                handlerFuncObject?.Invoke(obj);
                handlerFuncSimple?.Invoke();
            }
        }
    }
}
