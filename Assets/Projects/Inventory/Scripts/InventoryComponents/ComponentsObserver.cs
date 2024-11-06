
using UnityEngine;
using Zenject;

namespace Inventory
{
    public sealed class ComponentsObserver : ILateDisposable
    {
        private readonly InventoryEventNotifier _eventNotifier;
        private readonly IEntity _entity;

        public ComponentsObserver(
            InventoryEventNotifier eventNotifier,
            IEntity entity)
        {
            _eventNotifier = eventNotifier;
            _entity = entity;
            
            _eventNotifier.OnItemConsumed += HandleItemAdded;
            _eventNotifier.OnItemEquipped += HandleItemAdded;
            _eventNotifier.OnItemUnequipped += HandleItemUnequipped;
            
        }

        public void HandleItemUnequipped(InventoryItem item, EquipmentSlot equipmentSlot)
        {
            foreach (IItemComponent component in item.itemComponents)
            {
                component.Remove(_entity);
            }
        }
        
        public void HandleItemAdded(InventoryItem item, EquipmentSlot _)
        {
            foreach (IItemComponent component in item.itemComponents)
            {
                component.Apply(_entity);
            }
        }
        
        public void HandleItemAdded(InventoryItem item)
        {
            foreach (IItemComponent component in item.itemComponents)
            {
                component.Apply(_entity);
            }
        }

        public void LateDispose()
        {
            _eventNotifier.OnItemConsumed -= HandleItemAdded;
            _eventNotifier.OnItemEquipped -= HandleItemAdded;
            _eventNotifier.OnItemUnequipped -= HandleItemUnequipped;
        }
    }
}