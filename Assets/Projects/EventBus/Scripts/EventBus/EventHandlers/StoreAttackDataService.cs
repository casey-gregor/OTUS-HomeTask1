using System.Collections.Generic;
using Zenject;

namespace EventBus
{
    public class StoreAttackDataService: IInitializable, ILateDisposable
    {
        public List<AttackedUsedData> Attacks = new();
        
        private readonly EventBus _eventBus;
        public StoreAttackDataService(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void HandleEvent(StoreAttackDataEvent evt)
        {
            Attacks.Add(new AttackedUsedData(evt.Target, evt.DamageTaken));
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<StoreAttackDataEvent>(HandleEvent);
        }


        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<StoreAttackDataEvent>(HandleEvent);
        }

        public void ClearEffects()
        {
            Attacks.Clear();
        }
    }
}