using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        }

    }

}
