using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float remainingTime;

    private IEnumerator timerCoroutine;
    private MonoBehaviour context;

    public event Action TimeIsOver;

    public Timer(MonoBehaviour context) => this.context = context;

    public void Set(float time) => this.remainingTime = time;
    public void StartCountdown()
    {
        timerCoroutine = Countdown();
        this.context.StartCoroutine(timerCoroutine);
    }

    public void StopCountdown()
    {
        if(timerCoroutine != null)
        {
            this.context.StopCoroutine(timerCoroutine);
        }
    }

    public IEnumerator Countdown()
    {
        while(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        TimeIsOver?.Invoke();
    }

    public void OnPause()
    {
        StopCountdown();
    }

    public void OnResume()
    {
        StartCountdown();
    }
}
