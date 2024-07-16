using Zenject;

namespace ShootEmUp
{
    public class TimerFactory
    {
        private ListenersStorage listenersStorage;
        private DiContainer diContainer;
        public TimerFactory(ListenersStorage listenersStorage, DiContainer container)
        {
            this.listenersStorage = listenersStorage;
            this.diContainer = container;
        }

        public Timer CreateTimer()
        {
            Timer timer = this.diContainer.Instantiate<Timer>();
            this.listenersStorage.AddToListeners(timer);
            return timer;
        }
    }

}
