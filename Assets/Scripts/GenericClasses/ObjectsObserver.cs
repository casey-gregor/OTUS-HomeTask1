using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    void RemoveObject(GameObject obj);
}

public abstract class ObjectsObserver
{
    private IFactory _factory;

    protected ObjectsObserver(IFactory factory)
    {
        this._factory = factory;
    }
    public abstract void SubscribeToObject(GameObject obj);

}
