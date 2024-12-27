using System.Collections.Generic;

namespace Inventory
{
    public sealed class EquipmentSlot
    {
        public int Capacity;
        
        private readonly EquipmentSlotType _type;
        private readonly Dictionary<InventoryItem, int> _inventoryItems;
        private int _currentItemCount;

        public EquipmentSlot(EquipmentSlotType type, int capacity)
        {
            _type = type;
            Capacity = capacity;
            _inventoryItems = new();
        }
        
        public EquipmentSlotType GetSlotType() => _type;
       
        public bool TryAddItem(InventoryItem item)
        {
            if (!HasCapacity())
            {
                return false;
            }

            if((item.inventoryType & InventoryType.Wearable) == InventoryType.Wearable)
            {
                AddItem(item);
                return true;
            }

            return false;
        }
        
        public bool TryRemoveItem(InventoryItem item)
        {
            if (TryFindItem(item, out InventoryItem foundItem))
            {
                _inventoryItems.Remove(foundItem);

                _currentItemCount--;
                return true;
            }
            return false;
        }
        
        public bool TryFindItem(InventoryItem item, out InventoryItem foundItem)
        {
            foreach (InventoryItem key in _inventoryItems.Keys)
            {
                if (key.name == item.name)
                {
                    foundItem = key;
                    return true;
                }
            }
            foundItem = null;
            return false;
        }

        public Dictionary<InventoryItem, int> GetSlotItems() => _inventoryItems;
        
        private bool HasCapacity() => Capacity == -1 || _currentItemCount < Capacity;
        
        private void AddItem(InventoryItem item)
        {
            _inventoryItems.Add(item, 1);
            _currentItemCount++;
        }
    }
}