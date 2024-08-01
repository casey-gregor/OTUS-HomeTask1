using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameResumeListener : IGameListener
{
    void OnResume();
}
