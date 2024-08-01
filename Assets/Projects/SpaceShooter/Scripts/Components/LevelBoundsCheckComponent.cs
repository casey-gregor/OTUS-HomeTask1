using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class LevelBoundsCheckComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private Bullet bullet;

        public event Action<GameObject> OnOutOfBounds;
        public void OnFixedUpdate()
        {
            if (!bullet.isActive)
                return;
            if (!bullet.levelBounds.InBounds(this.transform.position))
            {
                OnOutOfBounds?.Invoke(this.gameObject);
                bullet.SetIsActive(false);
            }
        }
    }

}
