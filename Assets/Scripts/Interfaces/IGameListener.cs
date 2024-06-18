using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public interface IGameListener
{
    static event Action<IGameListener> RegisterEvent;
    static event Action<IGameListener> UnregisterEvent;
    static void Register(IGameListener gameListener)
    {
        //Debug.Log($"{gameListener} called Register");
        RegisterEvent?.Invoke(gameListener);
    }

    static void Unregister(IGameListener gameListener)
    {
        UnregisterEvent?.Invoke(gameListener);
    }
}
