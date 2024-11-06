using System.Collections.Generic;

namespace Inventory
{
    public sealed class Inventory
    {
        public int Capacity;
        private int _currentItemCount;
        public InventoryEventNotifier InventoryEventNotifier { get; }
    
        private readonly Dictionary<InventoryItem, int> _inventoryItems = new();
        public Inventory(
            int capacity,
            InventoryEventNotifier inventoryEventNotifier)
        {
            InventoryEventNotifier = inventoryEventNotifier;
            Capacity = capacity;
        }
        
        private bool HasCapacity() => Capacity == -1 || _currentItemCount < Capacity;

        
        public void AddItem(InventoryItem item)
        {
            if (item.inventoryType == InventoryType.None)
            {
                InventoryEventNotifier.NotifyInventoryTypeNone();
                return;
            }

            if (TryAddItem(item))
            {
                InventoryEventNotifier.NotifyItemAddedToInventory(item);
                InventoryEventNotifier.NotifyInventoryUpdated();
            }
            else
            {
                InventoryEventNotifier.NotifyItemAddedFailed(item.name);
            }
        }
        
        
        private bool TryAddItem(InventoryItem item)
        {
            if (!HasCapacity())
            {
                return false;
            }

            if (TryFindItem(item, out InventoryItem foundItem))
            {
                return HandleExistingItem(foundItem, item);
            }
            return AddNewItem(item);
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
        
        public bool TryRemoveItem(InventoryItem item)
        {
            if (TryFindItem(item, out InventoryItem foundItem))
            {
                if ((foundItem.inventoryType & InventoryType.Stackable) == InventoryType.Stackable
                    && _inventoryItems[foundItem] > 1)
                {
                    _inventoryItems[foundItem] -= 1;
                    _currentItemCount--;
                }
                else
                {
                    _inventoryItems.Remove(foundItem);
                }
                
                InventoryEventNotifier.NotifyItemRemovedFromInventory(item);
                InventoryEventNotifier.NotifyInventoryUpdated();
                return true;
            }
            InventoryEventNotifier.NotifyItemRemovedFailed(item.name);
            return false;
        }
        
        public bool TryConsumeItem(InventoryItem item)
        {
            if (CheckIfConsumable(item))
            {
                TryRemoveItem(item);
                InventoryEventNotifier.NotifyItemConsumed(item);
                return true;
            }
            InventoryEventNotifier.NotifyItemConsumedFailed(item.name);
            return false;
        }
        
        public bool CheckIfConsumable(InventoryItem item)
        {
            if ((item.inventoryType & InventoryType.Consumable) == InventoryType.Consumable)
            {
                return true;
            }
            return false;
        }
        
        public Dictionary<InventoryItem, int> GetInventoryItems() => _inventoryItems;
        
    }
}