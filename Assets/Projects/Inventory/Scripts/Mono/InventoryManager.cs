using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public sealed class InventoryManager : MonoBehaviour
    {
        public Action OnInventoryInitialized;
        
        public Entity entity;
        public ItemConfig config;
        [Min(-1)]
        public int inventoryCapacity = -1;// -1 is unlimited
        [Min(1)]
        public int headSlotCapacity = 1;
        [Min(1)]
        public int bodySlotCapacity = 1;
        [Min(1)]
        public int armsSlotCapacity = 2;
        [Min(1)]
        public int feetSlotCapacity = 2;
        
        public Inventory Inventory { get; private set; }
        public EquipmentManager EquipmentManager { get; private set; }
        
        [Inject]
        private void Construct(InventoryEventNotifier inventoryEventNotifier)
        {
            
            Inventory = new Inventory(inventoryCapacity, inventoryEventNotifier);
            EquipmentManager = new EquipmentManager(
                new EquipmentSlot(EquipmentSlotType.Head, headSlotCapacity),
                new EquipmentSlot(EquipmentSlotType.Body, bodySlotCapacity),
                new EquipmentSlot(EquipmentSlotType.Arms, armsSlotCapacity),
                new EquipmentSlot(EquipmentSlotType.Feet, feetSlotCapacity),
                Inventory);
            
            
            OnInventoryInitialized?.Invoke();
        }

        [Button]
        public void AddItemToInventory()
        {
            InventoryItem item = config.inventoryItem.Clone();
            if (item != null)
                Inventory.AddItem(item);
        }
        
        [Button]
        public void RemoveItemFromInventory()
        {
            Inventory.TryRemoveItem(config.inventoryItem);
        }

        [Button]
        public void ConsumeItem()
        {
            Inventory.TryConsumeItem(config.inventoryItem);
        }
        
        [Button]
        public void EquipItem()
        {
            EquipmentManager.TryEquipItem(config.inventoryItem);
        }
        
        [Button]
        public void UnequipItem()
        {
            EquipmentManager.TryUnequipItem(config.inventoryItem);
        }
    }
}