using System.Collections.Generic;

namespace Inventory
{
    public sealed class Inventory
    {
        public InventoryEventNotifier InventoryEventNotifier { get; }
        private readonly int _capacity;
        private readonly int _slotsNum;
    
        private readonly Dictionary<InventoryItem, int> _inventoryItems = new();
        public Inventory(
            int slotsNum,
            int capacity,
            InventoryEventNotifier inventoryEventNotifier)
        {
            InventoryEventNotifier = inventoryEventNotifier;
            _slotsNum = slotsNum;
            _capacity = capacity;
        }

        private bool HasCapacity(InventoryItem item) =>
            _capacity == -1 || _inventoryItems[item] < _capacity;

        private bool HasFreeSlots() => _inventoryItems.Count < _slotsNum;
        
        public void AddItem(InventoryItem item)
        {
            if (item.inventoryType == InventoryType.None)
            {
                InventoryEventNotifier.NotifyInventoryTypeNone();
                return;
            }

            if (TryAddItem(item))
            {
                InventoryEventNotifier.NotifyInventoryUpdated();
            }
            else
            {
                InventoryEventNotifier.NotifyItemAddedFailed(item.name);
            }
        }
        
        public bool TryFindItem(InventoryItem item, out InventoryItem foundItem)
        {
            foreach (InventoryItem key in _inventoryItems.Keys)
            {
                if (key.name == item.name && HasCapacity(key))
                {
                    foundItem = key;
                    return true;
                }
            }
            foundItem = null;
            return false;
        }
        
        public void RemoveItemCompletely(InventoryItem item)
        {
            if (RemoveFromInventory(item))
            {
                DestroyInventoryItem(item);
            }
        }

        public bool RemoveFromInventory(InventoryItem item)
        {
            if (TryFindItemToRemove(item, out InventoryItem foundItem))
            {
                if ((foundItem.inventoryType & InventoryType.Stackable) == InventoryType.Stackable
                    && _inventoryItems[foundItem] > 1)
                {
                    _inventoryItems[foundItem]--;
                    InventoryEventNotifier.NotifyStackableItemRemoved(_inventoryItems[foundItem], foundItem);
                }
                else
                {
                    _inventoryItems.Remove(foundItem);
                    InventoryEventNotifier.NotifyItemRemovedFromInventory(foundItem);
                }
                InventoryEventNotifier.NotifyInventoryUpdated();
                return true;
            }
            InventoryEventNotifier.NotifyItemRemovedFailed(item.name);
            return false;
        }

        private void DestroyInventoryItem(InventoryItem item)
        {
            item.Dispose();
        }
        
        public bool TryConsumeItem(InventoryItem item)
        {
            if (CheckIfConsumable(item))
            {
                InventoryEventNotifier.NotifyItemConsumed(item);
                RemoveItemCompletely(item);
                return true;
            }
            InventoryEventNotifier.NotifyItemConsumedFailed(item.name);
            return false;
        }
        
        public IReadOnlyDictionary<InventoryItem, int> GetInventoryItems() => _inventoryItems;
        
        public bool TryFindItemToRemove(InventoryItem item, out InventoryItem foundItem)
        {
            foundItem = null;
            foreach (InventoryItem key in _inventoryItems.Keys)
            {
                if (key.name == item.name)
                {
                    foundItem = key;
                }
            }
            return foundItem != null;
        }

        private bool CheckIfConsumable(InventoryItem item) =>
            (item.inventoryType & InventoryType.Consumable) == InventoryType.Consumable;
        
        
        private bool TryAddItem(InventoryItem item)
        {
            if ((item.inventoryType & InventoryType.Stackable) == InventoryType.Stackable)
            {
                return HandleStackableItem(item);
            }
            
            return AddNewItem(item);
        }
        
        private bool AddNewItem(InventoryItem item)
        {
            if (HasFreeSlots())
            {
                _inventoryItems[item] = 1;
                InventoryEventNotifier.NotifyItemAddedToInventory(item);
                return true;
            }
            return false;
        }
        
        private bool HandleStackableItem(InventoryItem item)
        {
            if (TryFindItem(item, out var foundItem))
            {
                _inventoryItems[foundItem] += 1;
                InventoryEventNotifier.NotifyExistingItemAddedToInventory(_inventoryItems[foundItem], foundItem);
                return true;
            }
    
            return AddNewItem(item);
        }
        
    }
}