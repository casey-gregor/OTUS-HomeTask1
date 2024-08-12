using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class Pool<T> where T : Component
    {
        private Queue<T> _objects;
        private T _prefab;
        private Transform _parent;
        private Transform _world;

        private int _count;

        public Pool(T prefab, int initialCount, Transform parent, Transform world)
        {
            _objects = new Queue<T>();
            _prefab = prefab;
            _parent = parent;
            _world = world;
            
            for (int i = 0; i < initialCount; i++)
            {
                T _object = GameObject.Instantiate(_prefab, _parent);
                _object.name = _prefab.name + _count;
                _count++;
                _objects.Enqueue(_object);
            }
        }

        public void Enqueue(T obj)
        {
            //Debug.Log("enqueue : " + obj.name);
            obj.transform.SetParent(_parent);
            _objects.Enqueue(obj);
        }

        public T GetObject()
        {
            _objects.TryDequeue(out T _object);
            if(_object == null)
            {
                _object = GameObject.Instantiate(_prefab, _parent);
                _object.name = _prefab.name + _count;
                _count++;
                //Debug.Log("instantiate new : " + _object.name);
            }
            _object.transform.SetParent(_world);
            //Debug.Log("get : " + _object.name);
            return _object;
        }
        
    }
}