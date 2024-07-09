using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Pool
    {
        public readonly Queue<GameObject> inactivateItems;
        public readonly HashSet<GameObject> activeItems;

        private GameObject prefab;
        private Transform container;
        private DiContainer diContainer;

        private int itemCount;
        public Pool(GameObject prefab, int initialCount, Transform container, DiContainer diContainer)
        {
            this.prefab = prefab;
            this.container = container;
            this.diContainer = diContainer;

            this.inactivateItems = new Queue<GameObject>();
            this.activeItems = new HashSet<GameObject>();

            for (int i = 0; i < initialCount; i++)
            {
                GameObject item = GameObject.Instantiate(prefab, container);
                item.name = $"{prefab.name}{i}";
                this.itemCount++;
                this.diContainer.InjectGameObject(item);
                this.inactivateItems.Enqueue(item);
            }
        }

        public GameObject GetItem()
        {
            this.inactivateItems.TryDequeue(out var item);

            if (item == null)
            {
                item = GameObject.Instantiate(this.prefab, this.container);
                item.name = $"{prefab.name}{this.itemCount++}";

                this.diContainer.InjectGameObject(item);
            }
            this.activeItems.Add(item);
            return item;
        }

        public void EnqueueItem(GameObject item)
        {
            if (this.activeItems.Contains(item))
            {
                item.transform.SetParent(this.container);
                this.inactivateItems.Enqueue(item);
                this.activeItems.Remove(item);
            }
        }
    }
}

