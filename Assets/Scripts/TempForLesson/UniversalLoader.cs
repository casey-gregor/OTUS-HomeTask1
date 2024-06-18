using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UniversalLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] loadOrder;

    //private void Start()
    //{
    //    LoadComponents();
    //}

    private void LoadComponents()
    {
        foreach (GameObject loadable in loadOrder)
        {
            if (loadable.TryGetComponent<ILoadable>(out ILoadable component) != false)
            {
                component.Load();
            }
        }
    }
}
