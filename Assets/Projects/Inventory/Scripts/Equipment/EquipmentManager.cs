using System.Collections.Generic;

namespace Inventory
{
    public sealed class EquipmentManager
    {
        private readonly Dictionary<EquipmentSlotType, EquipmentSlot> _equipmentSlots = new();
        
        private readonly Inventory _inventory;
        private readonly EquipmentEventNotifier _equipmentEventNotifier;

        public EquipmentManager(
            EquipmentSlot headSlot, 
            EquipmentSlot bodySlot, 
            EquipmentSlot rightHandSlot,
            EquipmentSlot leftHandSlot,
            EquipmentSlot feetSlot,
            Inventory inventory, 
            EquipmentEventNotifier equipmentEventNotifier)
        {
            _inventory = inventory;
            _equipmentEventNotifier = equipmentEventNotifier;

            _equipmentSlots.Add(EquipmentSlotType.Head, headSlot);
            _equipmentSlots.Add(EquipmentSlotType.Body, bodySlot);
            _equipmentSlots.Add(EquipmentSlotType.RightHand, rightHandSlot);
            _equipmentSlots.Add(EquipmentSlotType.LeftHand, leftHandSlot);
            _equipmentSlots.Add(EquipmentSlotType.Feet, feetSlot);
        }
        
        public bool TryEquipItem(InventoryItem item)
        {
            if (_inventory.TryFindItem(item, out var foundItem))
            {
                var slot = GetSlotFromDict(foundItem.equipmentSlotType);
                if (slot != default && slot.TryAddItem(foundItem))
                {
                    _inventory.RemoveFromInventory(foundItem);
                    _equipmentEventNotifier.NotifyItemEquipped(foundItem, slot);
                    return true;
                }
            }

            _equipmentEventNotifier.NotifyItemNotEquippable(item.name);
            return false;
        }

        public bool TryUnequipItem(InventoryItem item)
        {
            var slot = GetSlotFromDict(item.equipmentSlotType);
            if (slot != default && slot.TryFindItem(item, out var foundItem))
            {
                if (slot.TryRemoveItem(foundItem))
                {
                    _equipmentEventNotifier.NotifyItemUnequipped(foundItem, slot);
                    
                    _inventory.AddItem(item);
                    return true;
                }
            }

            _equipmentEventNotifier.NotifyItemNotEquipped(item.name);
            return false;
        }

        public EquipmentSlot GetSlotFromDict(EquipmentSlotType equipmentSlotType)
        {
            if (_equipmentSlots.TryGetValue(equipmentSlotType, out var slot))
            {
                return slot;
            }
            return default;
        }
        
    }
}