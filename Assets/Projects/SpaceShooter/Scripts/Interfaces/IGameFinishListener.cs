using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFinishListener : IGameListener
{
    void OnFinish();
}
