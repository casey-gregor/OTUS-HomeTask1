using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenersStorage
{
    public List<IGameListener> gameListeners = new();

    public ListenersStorage()
    {
        //Debug.Log("ListenersStorage construct ");
        IGameListener.RegisterEvent += RegisterEventHandler;
    }

    private void RegisterEventHandler(IGameListener gameListener)
    {
        gameListeners.Add(gameListener);
        //Debug.Log("added from ListenersStorage" + gameListener);
    }
}
