using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class InterfaceCollector
    {
        public List<IGameListener> Listeners { get { return listeners; } }
        private List<IGameListener> listeners = new();
        private List<Type> typesWithInterfaces = new();
        private DiContainer diContainer;
        private ListenersStorage listenersStorage;
        public InterfaceCollector(DiContainer container, ListenersStorage listenersStorage)
        {
            this.diContainer = container;
            this.listenersStorage = listenersStorage;
            UpdateList();
        }

        public void UpdateList()
        {
            this.typesWithInterfaces = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                   where t.IsClass && t.GetInterfaces().Contains(typeof(IGameListener))
                                   select t).ToList();

            foreach (var type in typesWithInterfaces)
            {
                if (this.diContainer.HasBinding(type))
                {
                    var instance = diContainer.Resolve(type) as IGameListener;
                    if (instance != null)
                    {
                        this.listenersStorage.AddToListeners(instance);
                    }
                }
            }

            //Alternative implementation with Zenject container.////
            //var allBindings = this.diContainer.AllContracts;
            //List<IGameListener> _listeners = new List<IGameListener>();
            //foreach (var binding in allBindings)
            //{
            //    var contractType = binding.Type;
            //    //Debug.Log("contractType : " + binding);

            //    if (typeof(IGameListener).IsAssignableFrom(contractType))
            //    {
            //        var resolvedInstance = this.diContainer.ResolveAll(contractType).OfType<IGameListener>();
                    

            //        foreach( var instance in resolvedInstance)
            //        {
                        

            //            if (resolvedInstance != null && !_listeners.Contains(instance))
            //            {
            //               _listeners.Add(instance);
            //                Debug.Log("resolved instance : " + instance);
            //            }
            //        }
            //    }
            //}
        }

    }

}
