using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePauseListener : IGameListener
{
    void OnPause();
}
