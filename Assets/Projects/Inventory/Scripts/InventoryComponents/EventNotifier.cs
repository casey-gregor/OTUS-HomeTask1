using System;

namespace Inventory
{
    public sealed class EventNotifier
    {
        public Action<InventorySlot> OnSlotUpdated;
        public Action<string> OnItemAddFailed;
        public Action<string> OnItemConsumeFailed;
        public Action<string> OnItemRemoveFailed;
        public Action OnInventoryTypeNone;
        
        public Action<InventoryItem> OnItemConsumed;
        public Action<InventoryItem> OnWearableAdded;
        public Action<InventoryItem> OnWearableRemoved;
        public Action<InventoryItem, InventorySlot> OnItemAdded;
        public Action<InventoryItem, InventorySlot> OnItemRemoved;
        
        public void NotifySlotUpdated(InventorySlot slot) => OnSlotUpdated?.Invoke((InventorySlot)slot);
        public void NotifyItemAddedFailed(string itemName) => OnItemAddFailed?.Invoke(itemName);
        public void NotifyItemConsumedFailed(string itemName) => OnItemConsumeFailed?.Invoke(itemName);
        public void NotifyItemRemovedFailed(string itemName) => OnItemRemoveFailed?.Invoke(itemName);
        public void NotifyInventoryTypeNone() => OnInventoryTypeNone?.Invoke();
        public void NotifyItemConsumed(InventoryItem item) => OnItemConsumed?.Invoke(item);
        public void NotifyWearableAdded(InventoryItem item) => OnWearableAdded?.Invoke(item);
        public void NotifyWearableRemoved(InventoryItem item) => OnWearableRemoved?.Invoke(item);
        public void NotifyItemAdded(InventoryItem item, InventorySlot slot) => 
            OnItemAdded?.Invoke(item,slot);
        public void NotifyItemRemoved(InventoryItem item, InventorySlot slot) => 
            OnItemRemoved?.Invoke(item,slot);
    }
}