using System;

namespace Inventory
{
    public class EquipmentEventNotifier
    {
        public event Action<InventoryItem, EquipmentSlot> OnItemEquipped;
        public event Action<InventoryItem, EquipmentSlot> OnItemUnequipped;
        public event Action<string> OnItemNotEquipped;
        public event Action<string> OnItemNotWearable;
        
        public void NotifyItemEquipped(InventoryItem item, EquipmentSlot slot) => 
            OnItemEquipped?.Invoke(item,slot);
        public void NotifyItemUnequipped(InventoryItem item, EquipmentSlot slot) => 
            OnItemUnequipped?.Invoke(item,slot);
        
        public void NotifyItemNotEquippable(string itemName) => 
            OnItemNotWearable?.Invoke(itemName);
        public void NotifyItemNotEquipped(string itemName) => 
            OnItemNotEquipped?.Invoke(itemName);
    }
}