using ShootEmUp;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBoundsCheckComponent : IGameFixedUpdateListener
{
    private HashSet<GameObject> bulletsToCheck;
    private HashSet<GameObject> itemsToRemove;
    private BulletSpawnerComponent bulletSpawner;
    private LevelBoundsComponent levelBounds;
    
    public event Action<GameObject> OnOutOfBounds;

    public LevelBoundsCheckComponent(LevelBoundsComponent levelBounds)
    {
        bulletsToCheck = new HashSet<GameObject>();
        //this.bulletSpawner = bulletSpawner;
        this.levelBounds = levelBounds;

        itemsToRemove = new HashSet<GameObject>();
        IGameListener.Register(this);
    }

    public void SubscribeToSpawner(BulletSpawnerComponent bulletSpawner)
    {
        bulletSpawner.bulletSpawnEvent += HandleBulletSpawnEvent;
    }

    private void HandleBulletSpawnEvent(GameObject obj)
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
