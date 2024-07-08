using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletPool
    {
        public readonly Queue<GameObject> inactiveItems;
        public readonly List<GameObject> activeItems;

        private BulletSpawnerConfig config;
        private Transform container;
        private DiContainer diContainer;

        private int itemCount;
        public BulletPool(BulletSpawnerConfig config, Transform container, DiContainer diContainer)
        {
            this.config = config;
            this.container = container;
            this.diContainer = diContainer;

            inactiveItems = new Queue<GameObject>();
            activeItems = new List<GameObject>();

            for (int i = 0; i < this.config.initialCount; i++)
            {
                GameObject item = GameObject.Instantiate(this.config.prefab, container);
                item.name = $"{this.config.prefab.name}{i}";
                itemCount++;
                this.diContainer.InjectGameObject(item);
                this.inactiveItems.Enqueue(item);
            }
        }

        public GameObject GetItem()
        {
            inactiveItems.TryDequeue(out var item);

            if (item == null)
            {
                item = GameObject.Instantiate(this.config.prefab, this.container);
                item.name = $"{this.config.prefab.name}{itemCount++}";

                this.diContainer.InjectGameObject(item);
            }
            activeItems.Add(item);
            return item;
        }

        public void EnqueueItem(GameObject item)
        {
            if (activeItems.Contains(item))
            {
                item.transform.SetParent(this.container);
                inactiveItems.Enqueue(item);
                activeItems.Remove(item);
            }

        }
    }
}

