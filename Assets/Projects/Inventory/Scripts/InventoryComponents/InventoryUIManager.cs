using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryUIManager : IDisposable
    {
        private readonly ItemView _prefab;
        private readonly InventoryEventNotifier _inventoryNotifier;
        private readonly EquipmentEventNotifier _equipmentNotifier;
        private readonly UISlotProvider _uiSlotProvider;
        private readonly ItemInstantiator _itemInstantiator;

        public InventoryUIManager(
            ItemView prefab,
            UISlotProvider uiSlotProvider, 
            InventoryEventNotifier inventoryNotifier, 
            EquipmentEventNotifier equipmentNotifier)
        {
            _prefab = prefab;
            _uiSlotProvider = uiSlotProvider;
            _inventoryNotifier = inventoryNotifier;
            _equipmentNotifier = equipmentNotifier;

            _itemInstantiator = new ItemInstantiator();

            _inventoryNotifier.OnItemAddedToInventory += HandleNewItemEvent;
            _inventoryNotifier.OnExistingItemAdded += HandleExistingItemEvent;
            _inventoryNotifier.OnItemRemovedFromInventory += HandleItemRemoved;
            _inventoryNotifier.OnStackableItemRemoved += HandleStackableItemRemoved;
            
            _equipmentNotifier.OnItemEquipped += HandleItemEquipped;
            _equipmentNotifier.OnItemUnequipped += HandleItemUnequipped;
        }

        private void HandleItemUnequipped(InventoryItem item, EquipmentSlot slot)
        {
            item.RemoveItemView();
        }

        private void HandleItemEquipped(InventoryItem item, EquipmentSlot slot)
        {
            Transform equipmentSlot = _uiSlotProvider.GetEquipmentSlot(slot);
            _itemInstantiator.CreateItem(_prefab, item, equipmentSlot);
        }

        private void HandleItemRemoved(InventoryItem item)
        {
            item.RemoveItemView();
        }

        private void HandleStackableItemRemoved(int qty, InventoryItem item)
        {
            var itemView = item.GetItemView();
            if (qty <= 1)
            {
                itemView.ToggleQtyPopup(false);
                return;
            }
            itemView.UpdateQtyPopup(qty);
        }

        private void HandleNewItemEvent(InventoryItem item)
        {
            Transform inventorySlot = _uiSlotProvider.GetFreeInventorySlot();
            _itemInstantiator.CreateItem(_prefab, item, inventorySlot);
        }
        
        private void HandleExistingItemEvent(int qty, InventoryItem item)
        {
            var itemView = item.GetItemView();
            itemView.UpdateQtyPopup(qty);
            itemView.ToggleQtyPopup(true);
        }

        public void Dispose()
        {
            _inventoryNotifier.OnItemAddedToInventory -= HandleNewItemEvent;
            _inventoryNotifier.OnExistingItemAdded -= HandleExistingItemEvent;
            _inventoryNotifier.OnItemRemovedFromInventory -= HandleItemRemoved;
            _inventoryNotifier.OnStackableItemRemoved -= HandleStackableItemRemoved;
            
            _equipmentNotifier.OnItemEquipped -= HandleItemEquipped;
            _equipmentNotifier.OnItemUnequipped -= HandleItemUnequipped;
        }
    }
}