using ShootEmUp;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBoundsCheckComponent : IGameFixedUpdateListener
{
    private HashSet<GameObject> bulletsToCheck;
    private HashSet<GameObject> itemsToRemove;
    private BulletInitializeComponent bulletInitializeComponent;
    private LevelBoundsComponent levelBounds;
    
    public event Action<GameObject> OnOutOfBounds;

    public LevelBoundsCheckComponent(BulletInitializeComponent bulletInitializeComponent, LevelBoundsComponent levelBounds)
    {
        bulletsToCheck = new HashSet<GameObject>();
        itemsToRemove = new HashSet<GameObject>();
        this.bulletInitializeComponent = bulletInitializeComponent;
        this.levelBounds = levelBounds;

        this.bulletInitializeComponent.bulletToMoveEvent += HandleBulletToMoveEvent;

        IGameListener.Register(this);
    }

   
    private void HandleBulletToMoveEvent(GameObject obj, Vector2 _)
    {
        bulletsToCheck.Add(obj);
    }

    public void OnFixedUpdate()
    {
        itemsToRemove.Clear();

        foreach(GameObject obj in bulletsToCheck)
        {
            if (!levelBounds.IsInBounds(obj.transform.position))
            {
                //Debug.Log($"{obj.name} is out of bounds");
                OnOutOfBounds?.Invoke(obj);
                itemsToRemove.Add(obj);
            }
        }

        foreach(GameObject obj in itemsToRemove)
        {
            bulletsToCheck.Remove(obj);
        }
    }
}
