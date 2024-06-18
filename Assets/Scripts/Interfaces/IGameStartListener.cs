using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameStartListener : IGameListener
{
    void OnStart();
}
