using System;
using UnityEngine;
using Zenject;

public class TimerService : IGameFixedUpdateListener
{
    private GameObject obj;
    private float timer;
    private float elapsedTime;
    private Action handlerFuncSimple;
    private Action<GameObject> handlerFuncObject;
    public bool timerRunning { get; private set; }
    public TimerService()
    {
        IGameListener.Register(this);
    }

    public void Set(GameObject objSet, float timer, Action<GameObject> handlerFunc)
    {
        //Debug.Log($"obj timer started : {objSet.name}, {timer}, {handlerFunc.Method}");
        this.obj = objSet;
        this.timer = timer;
        elapsedTime = 0;
        timerRunning = true;
        this.handlerFuncObject = handlerFunc;
    }

    public void Set(float timer, Action handlerFunc)
    {
        //Debug.Log("non-obj timer started");
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
    //public void FixedTick()
    //{
    //    if (timerRunning)
    //    {
            
    //        elapsedTime += Time.fixedDeltaTime;
    //        if (elapsedTime >= timer)
    //        {
    //            Stop();
    //            //Debug.Log("elapsed time : " + elapsedTime);
    //            //Debug.Log("objTimer : " + objTimer);
    //            handlerFuncObject?.Invoke(obj);
    //            handlerFuncSimple?.Invoke();
    //        }
    //    }
    //}

    public void OnFixedUpdate()
    {
        if (timerRunning)
        {

            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= timer)
            {
                Stop();
                //Debug.Log("elapsed time : " + elapsedTime);
                //Debug.Log("objTimer : " + objTimer);
                handlerFuncObject?.Invoke(obj);
                handlerFuncSimple?.Invoke();
            }
        }
    }
}
