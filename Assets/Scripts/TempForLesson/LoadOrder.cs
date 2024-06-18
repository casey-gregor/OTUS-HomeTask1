using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadOrder", menuName = "ScriptableObjects/LoadOrder", order = 1)]
public class LoadOrder : ScriptableObject
{ 
    public GameObject[] loadables;
}
