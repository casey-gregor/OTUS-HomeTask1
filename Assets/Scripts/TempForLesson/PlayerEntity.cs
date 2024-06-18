using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ILoadable
{
    public void Load()
    {
        Debug.Log("Player loaded");
    }
}
