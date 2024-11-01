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
        
        public int headSlotCapacity = 1;
        public int bodySlotCapacity = 1;
        public int armsSlotCapacity = 2;
        public int feetSlotCapacity = 2;
        public int backpackSlotCapacity = -1;// -1 is unlimited
        
        public Inventory Inventory { get; private set; }
        public SlotsManager SlotsManager { get; private set; }
        
        [Inject]
        private void Construct(EventNotifier eventNotifier)
        {
            if (headSlotCapacity < -1 ||
                bodySlotCapacity < -1 ||
                armsSlotCapacity < -1 ||
                feetSlotCapacity < -1 ||
                backpackSlotCapacity < -1)
            {
                Debug.LogWarning("Incorrect capacity specified for one of the slots. Can be -1 or greater.");
            }
            
            SlotsManager = new SlotsManager(
                new InventorySlot(SlotType.Head, headSlotCapacity),
                new InventorySlot(SlotType.Body, bodySlotCapacity),
                new InventorySlot(SlotType.Arms, armsSlotCapacity),
                new InventorySlot(SlotType.Feet, feetSlotCapacity),
                new InventorySlot(SlotType.Backpack, backpackSlotCapacity));
            
            Inventory = new Inventory(eventNotifier, SlotsManager);
            
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
            Inventory.RemoveItem(config.inventoryItem);
        }

        [Button]
        public void ConsumeItemFromBackpack()
        {
            Inventory.ConsumeItemFromBackpack(config.inventoryItem);
        }
    }
}