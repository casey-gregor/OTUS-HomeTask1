using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PoolManager
    {
        public readonly Queue<GameObject> _itemsPool;
        private GameObject _prefab;
        private Transform _container;
        public PoolManager(GameObject prefab, int initialCount, Transform container)
        {
            this._prefab = prefab;
            this._container = container;

            _itemsPool = new Queue<GameObject>();
            for (int i = 0; i < initialCount; i++)
            {
                GameObject item = GameObject.Instantiate(prefab, container);
                _itemsPool.Enqueue(item);
            }
        }

        public GameObject GetItem()
        {
            _itemsPool.TryDequeue(out var item);
            if (item == null)
                item = GameObject.Instantiate(this._prefab, this._container);
            return item;
        }

        public void EnqueueItem(GameObject item)
        {
            item.transform.SetParent(this._container);
            _itemsPool.Enqueue(item);
        }
    }
}

