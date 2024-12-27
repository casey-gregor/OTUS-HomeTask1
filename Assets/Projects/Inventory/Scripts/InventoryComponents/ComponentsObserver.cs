using Zenject;

namespace Inventory
{
    public sealed class ComponentsObserver : ILateDisposable
    {
        private readonly InventoryEventNotifier _inventoryEventNotifier;
        private readonly EquipmentEventNotifier _equipmentEventNotifier;
        private readonly IEntity _entity;

        public ComponentsObserver(
            InventoryEventNotifier inventoryEventNotifier,
            EquipmentEventNotifier equipmentEventNotifier,
            IEntity entity)
        {
            _inventoryEventNotifier = inventoryEventNotifier;
            _equipmentEventNotifier = equipmentEventNotifier;
            _entity = entity;

            _inventoryEventNotifier.OnItemConsumed += HandleItemAdded;
            _equipmentEventNotifier.OnItemEquipped += HandleItemAdded;
            _equipmentEventNotifier.OnItemUnequipped += HandleItemUnequipped;
            
        }

        public void HandleItemUnequipped(InventoryItem item, EquipmentSlot equipmentSlot)
        {
            foreach (IItemComponent component in item.EffectComponents)
            {
                component.Remove(_entity);
            }
        }
        
        public void HandleItemAdded(InventoryItem item, EquipmentSlot _)
        {
            foreach (IItemComponent component in item.EffectComponents)
            {
                component.Apply(_entity);
            }
        }
        
        public void HandleItemAdded(InventoryItem item)
        {
            foreach (IItemComponent component in item.EffectComponents)
            {
                component.Apply(_entity);
            }
        }

        public void LateDispose()
        {
            _inventoryEventNotifier.OnItemConsumed -= HandleItemAdded;
            _equipmentEventNotifier.OnItemEquipped -= HandleItemAdded;
            _equipmentEventNotifier.OnItemUnequipped -= HandleItemUnequipped;
        }
    }
}