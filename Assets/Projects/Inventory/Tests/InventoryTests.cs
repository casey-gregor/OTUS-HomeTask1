using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Inventory
{
    public class TestCharacter : IEntity
    {
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }
        
        public void HandleAddEvent(InventoryItem item)
        {
            foreach (var component in item.itemComponents)
            {
                component.Apply(this);
            }
        }
        
        public void HandleRemoveEvent(InventoryItem item)
        {
            foreach (var component in item.itemComponents)
            {
                component.Remove(this);
            }
        }
    }
    
    
    [TestFixture]
    public sealed class InventoryTests
    {
       
        private Inventory CreateInventory(
            InventorySlot headSlot,
            InventorySlot bodySlot,
            InventorySlot armsSlot,
            InventorySlot feetSlot,
            InventorySlot backpackSlot)
        {
            EventNotifier notifier = new EventNotifier();
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            return new Inventory(notifier, manager);
        }

        private ItemConfig CreateItemConfig(
            string itemName, 
            InventoryType inventoryType, 
            SlotType slotType,
            List<IItemComponent> itemComponents)
        {
            var itemConfig = ScriptableObject.CreateInstance<ItemConfig>();
            InventoryItem item = new InventoryItem()
            {
                name = itemName,
                inventoryType = inventoryType,
                slotType = slotType,
                itemComponents = itemComponents
            };
            itemConfig.inventoryItem = item;
            return itemConfig;
        }
        
        public void AddItemToInventory(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            if(item != null)
                inventory.AddItem(item);
        }

        public void RemoveItemFromInventory(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            inventory.RemoveItem(item);
        }

        public void ConsumeItem(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            inventory.ConsumeItemFromBackpack(item);
        }

        [Test]
        public void SlotsAreNotNull()
        {
            //Arrange
            var bodySlot = new InventorySlot(SlotType.Body, 1);
            var headSlot = new InventorySlot(SlotType.Head, 1);
            var armsSlot = new InventorySlot(SlotType.Arms, 2);
            var feetSlot = new InventorySlot(SlotType.Feet, 2);
            var backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                bodySlot,
                headSlot,
                armsSlot,
                feetSlot,
                backpackSlot);
            
            //Assert
            Assert.IsNotNull(inventory.SlotsManager.GetSlot(SlotType.Body));
            Assert.IsNotNull(inventory.SlotsManager.GetSlot(SlotType.Head));
            Assert.IsNotNull(inventory.SlotsManager.GetSlot(SlotType.Arms));
            Assert.IsNotNull(inventory.SlotsManager.GetSlot(SlotType.Feet));
            Assert.IsNotNull(inventory.SlotsManager.GetSlot(SlotType.Backpack));
        }
        
        [Test]
        public void ItemWithSpecificSlotTypeNeedToGoToCorrespondingSlot()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            // //Act
            AddItemToInventory(inventory, itemConfig);
            
            // //Assert
            Assert.AreEqual(1, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }

        [Test]
        public void IfSlotIsFullThenAddToBackpack()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(1, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void IfBackpackCapacityIsFullThenNotAddItem()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, 1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void ItemRemovedFromHeadSlot()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void RemoveItemFirstFromBackpack()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(1, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void RemoveItemFirstFromBackpackThenFromHeadSlot()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void RemoveItemFromBackpackIfConsumed()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            ConsumeItem(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void ItemCanBeConsumedOnlyFromBackpack()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                SlotType.Body,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            ConsumeItem(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void OnlyConsumableItemCanBeConsumed()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Wearable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            ConsumeItem(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void ConsumableItemCanBeConsumedWithoutAddingToBackpack()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            ConsumeItem(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void OnlyStackableItemCanBeStacked()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Stackable, 
                SlotType.Backpack,
                itemComponents);
            
            int foundItemCount = 0;
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            foreach (InventoryItem item in backpackSlot.GetInventoryItems().Keys)
            {
                if (item.name == itemConfig.inventoryItem.name)
                {
                    foundItemCount = backpackSlot.GetInventoryItems()[item];
                }
            }
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(2, foundItemCount);
        }
        
        [Test]
        public void RemoveStackableItemNeedToBeEqualToNumberOfItemsInSlot()
        {
            //Arrange
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            var inventory = CreateInventory(
                headSlot: headSlot,
                bodySlot: bodySlot,
                armsSlot: armsSlot,
                feetSlot: feetSlot,
                backpackSlot: backpackSlot);

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Stackable, 
                SlotType.Backpack,
                itemComponents);

            int foundItemCount = 0;
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            foreach (InventoryItem item in backpackSlot.GetInventoryItems().Keys)
            {
                if (item.name == itemConfig.inventoryItem.name)
                {
                    foundItemCount = backpackSlot.GetInventoryItems()[item];
                }
            }
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, foundItemCount);
        }

        [Test]
        public void ApplyEffectForWearableItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter();
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableAdded += testCharacter.HandleAddEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            eventNotifier.OnWearableAdded -= testCharacter.HandleAddEvent;
            
            //Assert
            Assert.AreEqual(1, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(10, testCharacter.Armor);
        }
        
        [Test]
        public void RemoveEffectFromWearableItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter();
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableRemoved += testCharacter.HandleRemoveEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            eventNotifier.OnWearableRemoved -= testCharacter.HandleRemoveEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, testCharacter.Armor);
        }
        
        [Test]
        public void RemoveArmorEffectNotGoBelowZeroItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Armor = -5
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableRemoved += testCharacter.HandleRemoveEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            eventNotifier.OnWearableRemoved -= testCharacter.HandleRemoveEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, testCharacter.Armor);
        }
        
        [Test]
        public void RemoveHealthEffectNotGoBelowZeroItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Health = -5
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableRemoved += testCharacter.HandleRemoveEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new HealthComponent() { Health = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            eventNotifier.OnWearableRemoved -= testCharacter.HandleRemoveEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, testCharacter.Health);
        }
        
        [Test]
        public void RemoveAttackEffectNotGoBelowZeroItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Attack = -5
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableRemoved += testCharacter.HandleRemoveEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new AttackComponent() { Attack = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            eventNotifier.OnWearableRemoved -= testCharacter.HandleRemoveEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, testCharacter.Attack);
        }
        
        [Test]
        public void RemoveSpeedEffectNotGoBelowZeroItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Speed = -5
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableRemoved += testCharacter.HandleRemoveEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new SpeedComponent() { Speed = 10 }
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            RemoveItemFromInventory(inventory, itemConfig);
            eventNotifier.OnWearableRemoved -= testCharacter.HandleRemoveEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, testCharacter.Speed);
        }
        
        [Test]
        public void ApplyEffectForConsumableItem()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Health = 0
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnItemConsumed += testCharacter.HandleAddEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                new HealthComponent() { Health = 20 }
            };
            
            var itemConfig = CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            ConsumeItem(inventory, itemConfig);
            eventNotifier.OnItemConsumed -= testCharacter.HandleAddEvent;
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(20, testCharacter.Health);
        }
        
        [Test]
        public void NotApplyEffectIfItemComponentIsNullAndNotThrowNullReference()
        {
            //Arrange
            TestCharacter testCharacter = new TestCharacter()
            {
                Speed = 1
            };
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();

            eventNotifier.OnWearableAdded += testCharacter.HandleAddEvent;
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            eventNotifier.OnWearableAdded -= testCharacter.HandleAddEvent;
            
            //Assert
            Assert.AreEqual(1, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, testCharacter.Speed);
        }

        [Test]
        public void NotAddItemWithNoneSlotType()
        {
            
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.None, 
                SlotType.Head,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }

        [Test]
        public void NotAddItemIfBackpackSlotIsFull()
        {
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, 1);
            
            EventNotifier eventNotifier = new EventNotifier();
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(1, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void ThrowErrorIfSlotCapacityIsGreaterThanMinusOne()
        {
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -2);
            
            EventNotifier eventNotifier = new EventNotifier();
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = CreateItemConfig(
                "Helmet", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
        
        [Test]
        public void ItemIsNotAddedIfNameIsEmpty()
        {
            InventorySlot bodySlot = new InventorySlot(SlotType.Body, 1);
            InventorySlot headSlot = new InventorySlot(SlotType.Head, 1);
            InventorySlot armsSlot = new InventorySlot(SlotType.Arms, 2);
            InventorySlot feetSlot = new InventorySlot(SlotType.Feet, 2);
            InventorySlot backpackSlot = new InventorySlot(SlotType.Backpack, -1);
            
            EventNotifier eventNotifier = new EventNotifier();
            
            SlotsManager manager = new SlotsManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot, 
                backpackSlot);
            
            Inventory inventory = new Inventory(eventNotifier, manager);

            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = CreateItemConfig(
                "", 
                InventoryType.Consumable, 
                SlotType.Backpack,
                itemComponents);
            
            //Act
            AddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, headSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, bodySlot.GetInventoryItems().Count);
            Assert.AreEqual(0, armsSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, feetSlot.GetInventoryItems().Count);
            Assert.AreEqual(0, backpackSlot.GetInventoryItems().Count);
        }
    }
}

