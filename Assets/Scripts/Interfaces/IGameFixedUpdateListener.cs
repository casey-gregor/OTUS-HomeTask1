using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFixedUpdateListener : IGameListener
{
    void OnFixedUpdate();
}
