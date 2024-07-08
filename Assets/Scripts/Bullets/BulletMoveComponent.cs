using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveComponent : IGamePauseListener, IGameResumeListener, IGameFinishListener
{
    private BulletInitializeComponent bulletInitializeComponent;
    private Dictionary<GameObject, Vector2> bulletsToMove;
    public BulletMoveComponent(BulletInitializeComponent bulletInitializeComponent)
    {
        this.bulletInitializeComponent = bulletInitializeComponent;

        this.bulletInitializeComponent.bulletToMoveEvent += HandleBulletToMoveEvent;

        bulletsToMove = new Dictionary<GameObject, Vector2>();

        IGameListener.Register(this);
    }

    private void HandleBulletToMoveEvent(GameObject obj, Vector2 velocity)
    {
        if(bulletsToMove.ContainsKey(obj))
        {
            bulletsToMove[obj] = velocity;
        }
        else
        {
            bulletsToMove.Add(obj, velocity);
        }
    }
    public void OnFinish()
    {
        foreach (GameObject obj in bulletsToMove.Keys)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        bulletsToMove.Clear();
    }

    public void OnPause()
    {
        foreach (GameObject obj in bulletsToMove.Keys)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void OnResume()
    {
        foreach (GameObject obj in bulletsToMove.Keys)
        {
            obj.GetComponent<Rigidbody2D>().velocity = bulletsToMove[obj];
        }
    }
}
