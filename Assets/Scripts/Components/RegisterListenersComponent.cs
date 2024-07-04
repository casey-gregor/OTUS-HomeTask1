using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterListenersComponent
{
    public void RegisterListeners(GameObject obj)
    {
        IGameListener[] listeners = obj.GetComponents<IGameListener>();

        foreach (IGameListener listener in listeners)
        {
            IGameListener.Register(listener);
        }
    }

    public void UnregisterListeners(GameObject obj)
    {
        IGameListener[] listeners = obj.GetComponents<IGameListener>();

        foreach (IGameListener listener in listeners)
        {
            IGameListener.Unregister(listener);
        }
    }
}
