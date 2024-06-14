using ShootEmUp;
using UnityEngine;

public class EnemyFactory : PoolManager, IFactory
{
    public EnemyFactory(GameObject prefab, int initialCount, Transform container) : base(prefab, initialCount, container) { }

    public void RemoveObject(GameObject enemy)
    {
        EnqueueItem(enemy);
    }

}
