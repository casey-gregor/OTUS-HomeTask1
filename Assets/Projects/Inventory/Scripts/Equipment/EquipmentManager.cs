using System.Collections.Generic;

namespace Inventory
{
    public sealed class EquipmentManager
    {
        private readonly Dictionary<EquipmentSlotType, EquipmentSlot> _equipmentSlots = new();
        
        private readonly Inventory _inventory;

        public EquipmentManager(
            EquipmentSlot headSlot, 
            EquipmentSlot bodySlot, 
            EquipmentSlot armsSlot, 
            EquipmentSlot feetSlot, 
            Inventory inventory)
        {
            _inventory = inventory;

            _equipmentSlots.Add(EquipmentSlotType.Head, headSlot);
            _equipmentSlots.Add(EquipmentSlotType.Body, bodySlot);
            _equipmentSlots.Add(EquipmentSlotType.Arms, armsSlot);
            _equipmentSlots.Add(EquipmentSlotType.Feet, feetSlot);
        }
        
        public bool TryEquipItem(InventoryItem item)
        {
            if (_inventory.TryFindItem(item, out var foundItem))
            {
                var slot = GetSlotFromDict(item.equipmentSlotType);
                if (slot != default && slot.TryAddItem(foundItem))
                {
                    _inventory.InventoryEventNotifier.NotifyItemEquipped(foundItem, slot);
                    _inventory.TryRemoveItem(foundItem);
                    return true;
                }
            }

            _inventory.InventoryEventNotifier.NotifyItemNotEquippable(item.name);
            return false;
        }

        public bool TryUnequipItem(InventoryItem item)
        {
            var slot = GetSlotFromDict(item.equipmentSlotType);
            if (slot != default && slot.TryFindItem(item, out var foundItem))
            {
                if (slot.TryRemoveItem(foundItem))
                {
                    _inventory.InventoryEventNotifier.NotifyItemUnequipped(foundItem, slot);
                    _inventory.AddItem(item);
                    return true;
                }
            }

            _inventory.InventoryEventNotifier.NotifyItemNotEquipped(item.name);
            return false;
        }

        public EquipmentSlot GetSlotFromDict(EquipmentSlotType equipmentSlotType)
        {
            if(_equipmentSlots.TryGetValue(equipmentSlotType, out var slot))
                return slot;
            return default;
        }
        
    }
}