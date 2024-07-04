using ShootEmUp;
using System;
using UnityEngine;
using Zenject;

public class LevelBoundsCheckMono : MonoBehaviour, IGameFixedUpdateListener
{
    [SerializeField]
    private BulletMono bullet;
    
    public event Action<GameObject> OnOutOfBounds;

    //[Inject]
    //private void Construct(LevelBounds levelBounds)
    //{
    //    this.levelBounds = levelBounds;

    //}
    public void OnFixedUpdate()
    {
        if (!this.bullet.gameObject.transform.parent.gameObject.activeSelf)
            return;
        //Debug.Log($"{this.bullet.name} isActive : " + this.bullet.isActiveAndEnabled);
        if (!bullet.levelBounds.IsInBounds(this.transform.position))
        {
            //OnOutOfBounds?.Invoke(this.gameObject);
        }
    }
}
