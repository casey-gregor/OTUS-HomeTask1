using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletPool
    {
        public readonly Queue<GameObject> itemsPool;
        public readonly Dictionary<GameObject, List<Object>> itemComponents;
        private GameObject prefab;
        private Transform container;
        private DiContainer diContainer;

        private int itemCount;
        public BulletPool(GameObject prefab, int initialCount, Transform container, DiContainer diContainer)
        {
            this.prefab = prefab;
            this.container = container;
            //Debug.Log("bullet container : " + this.container.name);
            this.diContainer = diContainer;

            itemsPool = new Queue<GameObject>();
            itemComponents = new Dictionary<GameObject, List<Object>>();

            for (int i = 0; i < initialCount; i++)
            {
                GameObject item = GameObject.Instantiate(prefab, container);
                item.name = $"{prefab.name}{i}";
                itemCount++;
                this.diContainer.InjectGameObject(item);
                //Debug.Log("injected in pool : " + item.name);
                itemComponents[item] = new List<Object>();
                //ResolveComponents(item);
                this.itemsPool.Enqueue(item);
            }
        }

        public GameObject GetItem()
        {
            itemsPool.TryDequeue(out var item);
            //Debug.Log("in pool get item is " + item);
            //if(item != null)
            //{
            //    Debug.Log("items in pool : " + itemsPool.Count);
            //    if(itemsPool.Count > 0)
            //    {
            //        foreach(var _item in itemsPool)
            //        {
            //            Debug.Log($"item {_item.name} still in pool");
            //        }
            //    }
            //}
            if (item == null)
            {
                item = GameObject.Instantiate(this.prefab, this.container);
                item.name = $"{prefab.name}{itemCount++}";
                //Debug.Log("pool instantiated new bullet : " + item.name);
                this.diContainer.InjectGameObject(item);
            }
            return item;
        }

        public void EnqueueItem(GameObject item)
        {
            //Debug.Log("calling Enqueue");
            item.transform.SetParent(this.container);
            itemsPool.Enqueue(item);
            //Debug.Log($"{item.name} is enqueued");
            //Debug.Log($"{item.name} parent is {this.container.name}");
        }
    }
}

