using ShootEmUp;
using UnityEngine;

public class BulletFactory : PoolManager, IFactory
{
    public BulletFactory(GameObject prefab, int initialCount, Transform container) : base(prefab, initialCount, container) { }
    public void RemoveObject(GameObject bullet)
    {
        EnqueueItem(bullet);
    }



}
