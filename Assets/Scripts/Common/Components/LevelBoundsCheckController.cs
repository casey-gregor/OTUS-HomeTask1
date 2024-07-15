using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class LevelBoundsCheckController : IGameFixedUpdateListener, IDisposable
    {
        public event Action<GameObject> OnOutOfBounds;
    
        private HashSet<GameObject> bulletsToCheck;
        private HashSet<GameObject> itemsToRemove;
        private BulletInitializeComponent bulletInitializeComponent;
        private LevelBoundsController levelBounds;
    

        public LevelBoundsCheckController
            (
            BulletInitializeComponent bulletInitializeComponent, 
            LevelBoundsController levelBounds
            )
        {
            this.bulletsToCheck = new HashSet<GameObject>();
            this.itemsToRemove = new HashSet<GameObject>();

            this.bulletInitializeComponent = bulletInitializeComponent;
            this.levelBounds = levelBounds;

            this.bulletInitializeComponent.bulletToMoveEvent += HandleBulletToMoveEvent;

            IGameListener.Register(this);
        }

   
        private void HandleBulletToMoveEvent(GameObject bulletObject, Vector2 _)
        {
            this.bulletsToCheck.Add(bulletObject);
        }

        public void OnFixedUpdate()
        {
            this.itemsToRemove.Clear();

            foreach(GameObject bulletObject in this.bulletsToCheck)
            {
                if (!this.levelBounds.IsInBounds(bulletObject.transform.position))
                {
                    //Debug.Log($"{obj.name} is out of bounds");
                    this.OnOutOfBounds?.Invoke(bulletObject);
                    this.itemsToRemove.Add(bulletObject);
                }
            }

            foreach(GameObject obj in this.itemsToRemove)
            {
                this.bulletsToCheck.Remove(obj);
            }
        }

        public void Dispose()
        {
            this.bulletInitializeComponent.bulletToMoveEvent -= HandleBulletToMoveEvent;
        }
    }

}
