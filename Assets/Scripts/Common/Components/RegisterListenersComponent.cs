using UnityEngine;

namespace ShootEmUp
{
    public sealed class RegisterListenersComponent
    {
        public void RegisterListeners(GameObject obj)
        {
            IGameListener[] listeners = obj.GetComponents<IGameListener>();

            foreach (IGameListener listener in listeners)
            {
                IGameListener.Register(listener);
            }
        }

        public void UnregisterListeners(GameObject obj)
        {
            IGameListener[] listeners = obj.GetComponents<IGameListener>();

            foreach (IGameListener listener in listeners)
            {
                IGameListener.Unregister(listener);
            }
        }
    }

}
