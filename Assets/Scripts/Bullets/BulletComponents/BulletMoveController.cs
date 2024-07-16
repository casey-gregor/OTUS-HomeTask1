using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletMoveController : 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameFinishListener,
        IDisposable
    {
        private BulletInitializeController bulletInitializeComponent;
        private Dictionary<GameObject, Vector2> bulletsToMove;
        public BulletMoveController(BulletInitializeController bulletInitializeComponent)
        {
            this.bulletInitializeComponent = bulletInitializeComponent;

            this.bulletInitializeComponent.bulletToMoveEvent += HandleBulletToMoveEvent;

            this.bulletsToMove = new Dictionary<GameObject, Vector2>();
        }

        private void HandleBulletToMoveEvent(GameObject obj, Vector2 velocity)
        {
            if(this.bulletsToMove.ContainsKey(obj))
            {
                this.bulletsToMove[obj] = velocity;
            }
            else
            {
                this.bulletsToMove.Add(obj, velocity);
            }
        }
        public void OnFinish()
        {
            foreach (GameObject bulletObject in this.bulletsToMove.Keys)
            {
                bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            this.bulletsToMove.Clear();
        }

        public void OnPause()
        {
            foreach (GameObject bulletObject in this.bulletsToMove.Keys)
            {
                bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        public void OnResume()
        {
            foreach (GameObject bulletObject in this.bulletsToMove.Keys)
            {
                bulletObject.GetComponent<Rigidbody2D>().velocity = this.bulletsToMove[bulletObject];
            }
        }

        public void Dispose()
        {
            this.bulletInitializeComponent.bulletToMoveEvent -= HandleBulletToMoveEvent;
        }
    }

}
