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
        public ItemView itemPrefab;
        public Transform itemContainer;
        
        public int headSlotCapacity = 1;
        public int bodySlotCapacity = 1;
        public int armsSlotCapacity = 2;
        public int feetSlotCapacity = 2;
        public int backpackSlotCapacity = -1;// -1 is unlimited
        
        public Inventory Inventory { get; private set; }
        public SlotsManager SlotsManager { get; private set; }
        
        private ItemInstantiator _itemInstantiator;
        
        [Inject]
        private void Construct(EventNotifier eventNotifier)
        {
            SlotsManager = new SlotsManager(
                new InventorySlot(SlotType.Head, headSlotCapacity),
                new InventorySlot(SlotType.Body, bodySlotCapacity),
                new InventorySlot(SlotType.Arms, armsSlotCapacity),
                new InventorySlot(SlotType.Feet, feetSlotCapacity),
                new InventorySlot(SlotType.Backpack, backpackSlotCapacity));
            
            _itemInstantiator = new ItemInstantiator();
            
            Inventory = new Inventory(eventNotifier, SlotsManager);
            
            OnInventoryInitialized?.Invoke();
        }

        [Button]
        public void AddItemToInventory()
        {
            InventoryItem item = config.inventoryItem.Clone();
            _itemInstantiator.CreateItem(itemPrefab, item, itemContainer);
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