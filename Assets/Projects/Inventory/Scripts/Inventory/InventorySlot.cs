using System.Collections.Generic;

namespace Inventory
{
    public sealed class InventorySlot
    {
        public int Capacity;
        
        private readonly SlotType _type;
        private readonly Dictionary<InventoryItem, int> _inventoryItems;
        private int _currentItemCount;

        public InventorySlot(SlotType type, int capacity)
        {
            _type = type;
            Capacity = capacity;
            _inventoryItems = new();
        }
        
        public SlotType GetSlotType() => _type;
       
        public bool TryAddItem(InventoryItem item)
        {
            if (!HasCapacity())
            {
                return false;
            }

            if (TryGetItem(item, out InventoryItem foundItem))
            {
                return HandleExistingItem(foundItem, item);
            }
            return AddNewItem(item);
        }
        
        public bool TryRemoveItem(InventoryItem item)
        {
            if (TryGetItem(item, out InventoryItem foundItem))
            {
                if (_inventoryItems[foundItem] > 1)
                {
                    _inventoryItems[foundItem] -= 1;
                }
                else
                {
                    _inventoryItems.Remove(foundItem);
                }

                _currentItemCount--;
                return true;
            }
            return false;
        }

        public bool CheckIfConsumable(InventoryItem item)
        {
            if (TryGetItem(item, out InventoryItem foundItem) &&
                (foundItem.inventoryType & InventoryType.Consumable) == InventoryType.Consumable)
            {
                return true;
            }
            return false;
        }

        public Dictionary<InventoryItem, int> GetInventoryItems() => _inventoryItems;
        
        private bool HasCapacity() => Capacity == -1 || _currentItemCount < Capacity;
        
        private bool TryGetItem(InventoryItem item, out InventoryItem foundItem)
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
        
        private bool AddNewItem(InventoryItem item)
        {
            _inventoryItems[item] = 1;
            _currentItemCount++;
            return true;
        }
        
        private bool HandleExistingItem(InventoryItem foundItem, InventoryItem item)
        {
            if ((item.inventoryType & InventoryType.Stackable) == InventoryType.Stackable)
            {
                _inventoryItems[foundItem] += 1;
                _currentItemCount++;
                return true;
            }
    
            return AddNewItem(item);
        }
    }
}