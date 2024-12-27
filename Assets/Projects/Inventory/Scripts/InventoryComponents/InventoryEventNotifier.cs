using System;

namespace Inventory
{
    public sealed class InventoryEventNotifier
    {
        public event Action OnInventoryUpdated;
        public event Action<string> OnItemAddFailed;
        public event Action<string> OnItemConsumeFailed;
        public event Action<string> OnItemRemoveFailed;
        public event Action OnInventoryTypeNone;
        
        public event Action<InventoryItem> OnItemConsumed;
        public event Action<InventoryItem> OnItemAddedToInventory;
        public event Action<InventoryItem> OnItemRemovedFromInventory;
        
        //UI events
        public event Action<int, InventoryItem> OnExistingItemAdded;
        public event Action<int, InventoryItem> OnStackableItemRemoved;

       
        public void NotifyInventoryUpdated() => 
            OnInventoryUpdated?.Invoke();
        
        public void NotifyItemAddedFailed(string itemName) => 
            OnItemAddFailed?.Invoke(itemName);
        
        public void NotifyItemConsumedFailed(string itemName) => 
            OnItemConsumeFailed?.Invoke(itemName);
        
        public void NotifyItemRemovedFailed(string itemName) => 
            OnItemRemoveFailed?.Invoke(itemName);
        
        public void NotifyInventoryTypeNone() => 
            OnInventoryTypeNone?.Invoke();
        
        public void NotifyItemConsumed(InventoryItem item) => 
            OnItemConsumed?.Invoke(item);
        
        public void NotifyItemAddedToInventory(InventoryItem item) => 
            OnItemAddedToInventory?.Invoke(item);
        
        public void NotifyExistingItemAddedToInventory(int qty, InventoryItem item) => 
            OnExistingItemAdded?.Invoke(qty, item);
        
        public void NotifyItemRemovedFromInventory(InventoryItem item) => 
            OnItemRemovedFromInventory?.Invoke(item);
        
        public void NotifyStackableItemRemoved(int qty, InventoryItem item) => 
            OnStackableItemRemoved?.Invoke(qty, item);
        
        
    }
}