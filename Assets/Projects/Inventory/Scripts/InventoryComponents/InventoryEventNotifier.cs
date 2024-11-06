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
        public event Action<InventoryItem, EquipmentSlot> OnItemEquipped;
        public event Action<InventoryItem, EquipmentSlot> OnItemUnequipped;
        public event Action<string> OnItemNotEquipped;
        public event Action<string> OnItemNotWearable;

        public void NotifyItemNotEquippable(string itemName) => OnItemNotWearable?.Invoke(itemName);
        public void NotifyItemNotEquipped(string itemName) => OnItemNotEquipped?.Invoke(itemName);
        public void NotifyInventoryUpdated() => OnInventoryUpdated?.Invoke();
        public void NotifyItemAddedFailed(string itemName) => OnItemAddFailed?.Invoke(itemName);
        public void NotifyItemConsumedFailed(string itemName) => OnItemConsumeFailed?.Invoke(itemName);
        public void NotifyItemRemovedFailed(string itemName) => OnItemRemoveFailed?.Invoke(itemName);
        public void NotifyInventoryTypeNone() => OnInventoryTypeNone?.Invoke();
        public void NotifyItemConsumed(InventoryItem item) => OnItemConsumed?.Invoke(item);
        public void NotifyItemAddedToInventory(InventoryItem item) => 
            OnItemAddedToInventory?.Invoke(item);
        public void NotifyItemRemovedFromInventory(InventoryItem item) => 
            OnItemRemovedFromInventory?.Invoke(item);
        public void NotifyItemEquipped(InventoryItem item, EquipmentSlot slot) => 
            OnItemEquipped?.Invoke(item,slot);
        public void NotifyItemUnequipped(InventoryItem item, EquipmentSlot slot) => 
            OnItemUnequipped?.Invoke(item,slot);
    }
}