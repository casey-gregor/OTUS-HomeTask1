using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public sealed class InventoryLogger : MonoBehaviour
    {
        [SerializeField] private InventoryManager inventoryManager;
        private Inventory Inventory => inventoryManager.Inventory;
        private InventoryEventNotifier InventoryEventNotifier => Inventory.InventoryEventNotifier;
        
        private void Awake()
        {
            InventoryEventNotifier.OnInventoryUpdated += HandleInventoryChange;
            InventoryEventNotifier.OnInventoryTypeNone += HandleInventoryTypeNone;
            InventoryEventNotifier.OnItemEquipped += HandleItemEquipped;
            InventoryEventNotifier.OnItemAddFailed += HandleItemAddFailed;
            InventoryEventNotifier.OnItemConsumeFailed += HandleItemConsumedFailed;
            InventoryEventNotifier.OnItemRemoveFailed += HandleItemRemoveFailed;
            InventoryEventNotifier.OnItemUnequipped += HandleItemUnequipped;
            InventoryEventNotifier.OnItemConsumed += HandleItemConsumed;
            InventoryEventNotifier.OnItemRemovedFromInventory += HandleItemRemovedFromInventory;
            InventoryEventNotifier.OnItemNotEquipped += HandleItemNotEquipped;
            InventoryEventNotifier.OnItemNotWearable += HandleItemNotWearable;
        }

        private void HandleItemNotWearable(string itemName)
        {
            Debug.Log($"Item {itemName} can not be equipped.");
        }

        private void HandleItemNotEquipped(string itemName)
        {
            Debug.Log($"Item {itemName} is not equipped.");
        }

        private void HandleItemRemovedFromInventory(InventoryItem item)
        {
            Debug.Log($"Item {item.name} was removed from Inventory.");
        }

        private void HandleItemEquipped(InventoryItem item, EquipmentSlot slot)
        {
            Debug.Log($"Item {item.name} was added to {slot.GetSlotType()}.");
        }
        
        private void HandleItemUnequipped(InventoryItem item, EquipmentSlot slot)
        {
            Debug.Log($"Item {item.name} was removed from {slot.GetSlotType()}.");
        }
        
        private void HandleItemConsumed(InventoryItem item)
        {
            Debug.Log($"Item {item.name} was consumed.");
        }

        private void HandleItemRemoveFailed(string itemName)
        {
            Debug.Log($"Item {itemName} was not found in the Inventory.");
        }

        private void HandleItemConsumedFailed(string itemName)
        {
            Debug.Log($"Item {itemName} was not found in the Inventory or is not consumable.");
        }

        private void HandleItemAddFailed(string itemName)
        {
            Debug.LogWarning($"Could not add {itemName} to the inventory.");;
        }

        private void HandleInventoryTypeNone()
        {
            Debug.LogWarning("Cannot add item. 'None' type is set.");
        }

        private void HandleInventoryChange()
        {
            LogInventoryContents();
        }
        
        public void LogInventoryContents()
        {
            Dictionary<InventoryItem, int> inventoryItems = Inventory.GetInventoryItems();
            
            Dictionary<string, (int totalQuantity, int slotsOccupied)> inventorySummary = new();
            foreach (var entry in inventoryItems)
            {
                string itemName = entry.Key.name;
                int quantity = entry.Value;
                if (inventorySummary.ContainsKey(itemName))
                {
                    (int totalQuantity, int slotsOccupied) currentSummary = inventorySummary[itemName];
                    inventorySummary[itemName] = (
                        currentSummary.totalQuantity + quantity,
                        currentSummary.slotsOccupied + 1
                    );
                }
                else
                {
                    inventorySummary[itemName] = (
                        quantity,
                        1
                    );
                }
            }
            
            foreach (var summary in inventorySummary)
            {
                string itemName = summary.Key;
                int totalQuantity = summary.Value.totalQuantity;
                int slotsOccupied = summary.Value.slotsOccupied;
        
                Debug.Log($"{itemName} with qty of {totalQuantity} occupies {slotsOccupied} slot(s) of Inventory");
            }
            // Debug.Log($"slot {slot.GetSlotType()} size is {inventoryItems.Count}");
        }

        private void OnDestroy()
        {
            InventoryEventNotifier.OnInventoryUpdated -= HandleInventoryChange;
            InventoryEventNotifier.OnInventoryTypeNone -= HandleInventoryTypeNone;
            InventoryEventNotifier.OnItemEquipped -= HandleItemEquipped;
            InventoryEventNotifier.OnItemAddFailed -= HandleItemAddFailed;
            InventoryEventNotifier.OnItemConsumeFailed -= HandleItemConsumedFailed;
            InventoryEventNotifier.OnItemRemoveFailed -= HandleItemRemoveFailed;
            InventoryEventNotifier.OnItemUnequipped -= HandleItemUnequipped;
            InventoryEventNotifier.OnItemConsumed -= HandleItemConsumed;
            InventoryEventNotifier.OnItemRemovedFromInventory -= HandleItemRemovedFromInventory;
            InventoryEventNotifier.OnItemNotEquipped -= HandleItemNotEquipped;
        }
    }
}