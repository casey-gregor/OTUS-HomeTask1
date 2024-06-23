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
            //Debug.Log("IGameListener registered : " +  listener);
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
