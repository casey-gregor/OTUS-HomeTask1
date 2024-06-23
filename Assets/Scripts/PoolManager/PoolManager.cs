using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PoolManager
    {
        public readonly Queue<GameObject> itemsPool;
        private GameObject prefab;
        private Transform container;
        public PoolManager(GameObject prefab, int initialCount, Transform container)
        {
            this.prefab = prefab;
            this.container = container;

            itemsPool = new Queue<GameObject>();
            for (int i = 0; i < initialCount; i++)
            {
                GameObject item = GameObject.Instantiate(prefab, container);
                itemsPool.Enqueue(item);
            }
        }

        public GameObject GetItem()
        {
            itemsPool.TryDequeue(out var item);
            if (item == null)
                item = GameObject.Instantiate(this.prefab, this.container);
            return item;
        }

        public void EnqueueItem(GameObject item)
        {
            item.transform.SetParent(this.container);
            itemsPool.Enqueue(item);
        }
    }
}

