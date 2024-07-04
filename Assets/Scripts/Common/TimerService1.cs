using System;
using UnityEngine;
using Zenject;

public class TimerService1 : IGameFixedUpdateListener
{
    public class Factory : PlaceholderFactory<TimerService1> { }

    private GameObject obj;
    private float timer;
    private float elapsedTime;
    private Action handlerFuncSimple;
    private Action<GameObject> handlerFuncObject;
    public bool timerRunning { get; private set; }
    private bool objTimer;

    public TimerService1()
    {
        IGameListener.Register(this);
    }

    public void Set(GameObject objSet, float timer, Action<GameObject> handlerFunc)
    {
        //Debug.Log($"obj timer started : {objSet.name}, {timer}, {handlerFunc.Method}");
        objTimer = true;
        this.obj = objSet;
        this.timer = timer;
        elapsedTime = 0;
        timerRunning = true;
        this.handlerFuncObject = handlerFunc;
    }

    public void Resume()
    {
        //Debug.Log("resume");
        timerRunning = true;
    }

    public void Stop()
    {
        //Debug.Log("stop");
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
                //Debug.Log("elapsed time : " + elapsedTime);
                //Debug.Log("obj to invoke : " + this.obj);
                handlerFuncObject?.Invoke(this.obj);
            }
        }
    }
}
