using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Inventory
{
    
    [TestFixture]
    public sealed class InventoryTests
    {
        private Inventory _inventory;
        private EquipmentManager _equipmentManager;
        private InventoryEventNotifier _inventoryEventNotifier;
        private TestCharacter _testCharacter;
        private ComponentsObserver _componentsObserver;

        [SetUp]
        public void Construct()
        {
            _testCharacter = new TestCharacter();
            _inventoryEventNotifier = new InventoryEventNotifier();
            _componentsObserver = new ComponentsObserver(_inventoryEventNotifier, _testCharacter);
            _inventory = new Inventory(-1, _inventoryEventNotifier);
            
            var bodySlot = new EquipmentSlot(EquipmentSlotType.Body, 1);
            var headSlot = new EquipmentSlot(EquipmentSlotType.Head, 1);
            var armsSlot = new EquipmentSlot(EquipmentSlotType.Arms, 2);
            var feetSlot = new EquipmentSlot(EquipmentSlotType.Feet, 2);
            
            _equipmentManager = TestHelper.CreateEquipmentManager(
                bodySlot,
                headSlot,
                armsSlot,
                feetSlot,
                _inventory);
        }
        
        [Test]
        public void WhenTryEquipItem_AndItemIsWearableAndHasEquipmentSlotType_ThenAddToCorrectEquipmentSlot()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            // //Act
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            // //Assert
            Assert.AreEqual(1, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Body).GetSlotItems().Count);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Arms).GetSlotItems().Count);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Feet).GetSlotItems().Count);
        }
        
        [Test]
        public void WhenTryEquipItem_AndItemHasEquipmentSlotTypeNone_ThenNotEquipItem()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Consumable, 
                EquipmentSlotType.None,
                itemComponents);
            
            // //Act
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            // //Assert
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
        }

        [Test]
        public void WhenTryEquipItem_AndItemIsNotInInventory_ThenReturnFalseAndNotEquip()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            //Act
            var result = TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
        }
        
        [Test]
        public void WhenTryEquipItem_AndSlotIsFull_ThenNotEquipItem()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            //Act
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            var result = TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
        }
        
        [Test]
        public void WhenTryEquipItem_AndSlotIsNone_ThenNotEquipItem()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.None,
                itemComponents);
            
            //Act
            var result = TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
        }
        
        [Test]
        public void WhenTryUnequipItem_AndItemWasEquipped_ThenRemoveItemFromSlot()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Act
            TestHelper.TryUnequipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
        }

        [Test]
        public void WhenTryUnequipItem_AndItemWasEquipped_ThenAddItemToInventory()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);

            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Act
            TestHelper.TryUnequipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
        }
        
        [Test]
        public void WhenTryUnequipItem_AndItemWasNotEquipped_ThenDoNothing()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            
            //Act
            var result = TestHelper.TryUnequipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void WhenTryEquipItem_AndItemWasInInventory_ThenRemoveItemFromInventory()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
        }
        
        [Test]
        public void WhenTryConsumeItem_AndItemHasEffect_ThenApplyEffect()
        {
            //Arrange
            _testCharacter.Health = 0;
            
            var itemComponents = new List<IItemComponent>()
            {
                new HealthComponent() { Health = 10 }
            };
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                EquipmentSlotType.None,
                itemComponents);
            
            //Act
            TestHelper.TryConsumeItem(_inventory, itemConfig);
            _inventoryEventNotifier.OnItemConsumed -= _componentsObserver.HandleItemAdded;
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
            Assert.AreEqual(10, _testCharacter.Health);
        }
        
        [Test]
        public void WhenTryConsumeItem_AndItemIsInInventory_ThenRemoveItemFromInventory()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                EquipmentSlotType.None,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryConsumeItem(_inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
        }
        
        [Test]
        public void WhenTryConsumeItem_AndItemIsNotConsumable_ThenNotConsume()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            //Act
            var result = TestHelper.TryConsumeItem(_inventory, itemConfig);
            
            //Assert
            Assert.IsFalse(result);
        }
        
       
        [Test]
        public void WhenTryConsumeItem_AndItemIsNotInInventory_ThenConsumeItem()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Consumable, 
                EquipmentSlotType.None,
                itemComponents);
            
            //Act
            var result = TestHelper.TryConsumeItem(_inventory, itemConfig);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public void WhenTryAddStackableItem_AndSameItemIsInInventory_ThenIncrementSameItemCount()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Stackable, 
                EquipmentSlotType.None,
                itemComponents);
            
            int foundItemCount = 0;
            
            //Act
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            foreach (InventoryItem item in _inventory.GetInventoryItems().Keys)
            {
                if (item.name == itemConfig.inventoryItem.name)
                {
                    foundItemCount = _inventory.GetInventoryItems()[item];
                }
            }
            
            //Assert
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
            Assert.AreEqual(2, foundItemCount);
        }
        
        [Test]
        public void WhenRemoveStackableItem_AndTwoSuchItemsInInventory_ThenDecrementItemCount()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Stackable, 
                EquipmentSlotType.None,
                itemComponents);

            int foundItemCount = 0;
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryRemoveItemFromInventory(_inventory, itemConfig);
            foreach (InventoryItem item in _inventory.GetInventoryItems().Keys)
            {
                if (item.name == itemConfig.inventoryItem.name)
                {
                    foundItemCount = _inventory.GetInventoryItems()[item];
                }
            }
            
            //Assert
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
            Assert.AreEqual(1, foundItemCount);
        }
        
        [Test]
        public void WhenTryRemoveItem_AndItemQtyInInventoryIsZero_ThenDoNotGoBelowZero()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Potion", 
                InventoryType.Stackable, 
                EquipmentSlotType.None,
                itemComponents);

            // int foundItemCount = 0;
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryRemoveItemFromInventory(_inventory, itemConfig);
            TestHelper.TryRemoveItemFromInventory(_inventory, itemConfig);
            
            // foreach (InventoryItem item in _inventory.GetInventoryItems().Keys)
            // {
            //     if (item.name == itemConfig.inventoryItem.name)
            //     {
            //         foundItemCount = _inventory.GetInventoryItems()[item];
            //     }
            // }
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
            // Assert.AreEqual(1, foundItemCount);
        }

        [Test]
        public void WhenWearableItemIsEquipped_AndItemHasComponent_ThenApplyEffect()
        {
            //Arrange

            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            _inventoryEventNotifier.OnItemEquipped -= _componentsObserver.HandleItemAdded;
            
            //Assert
            Assert.AreEqual(1, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(10, _testCharacter.Armor);
        }
        
        [Test]
        public void WhenWearableItemIsUnequipped_AndItemHasComponent_ThenRemoveEffect()
        {
            //Arrange
           
            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            
            //Act
            TestHelper.TryUnequipItem(_equipmentManager, itemConfig);
            _inventoryEventNotifier.OnItemUnequipped -= _componentsObserver.HandleItemUnequipped;
            
            //Assert
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
            Assert.AreEqual(0, _testCharacter.Armor);
        }
        
        [Test]
        public void WhenWearableItemUnEquipped_AndItemHasComponent_ThenEntityValueShouldNotGoBelowZero()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>()
            {
                new ArmorComponent() { Armor = 10 }
            };
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            _testCharacter.Armor = 5;
            
            //Act
            TestHelper.TryUnequipItem(_equipmentManager, itemConfig);
            _inventoryEventNotifier.OnItemUnequipped -= _componentsObserver.HandleItemUnequipped;
            
            //Assert
            Assert.AreEqual(0, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(1, _inventory.GetInventoryItems().Count);
            Assert.AreEqual(0, _testCharacter.Armor);
        }
        
        [Test]
        public void WhenTryEquipWearableItem_AndItemComponentIsNull_ThenNotApplyEffectAndNotThrowNullException()
        {
            //Arrange
            _testCharacter.Armor = 5;
            
            var itemComponents = new List<IItemComponent>()
            {
                null
            };
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Act
            TestHelper.TryEquipItem(_equipmentManager, itemConfig);
            _inventoryEventNotifier.OnItemUnequipped -= _componentsObserver.HandleItemUnequipped;
            
            //Assert
            Assert.AreEqual(1, _equipmentManager.GetSlotFromDict(EquipmentSlotType.Head).GetSlotItems().Count);
            Assert.AreEqual(5, _testCharacter.Armor);
        }

        [Test]
        public void WhenTryAddItemToInventory_AndInventorySlotTypeIsNone_ThenNotAddItem()
        {
            //Arrange
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.None, 
                EquipmentSlotType.Head,
                itemComponents);
            
            //Act
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
        }

        [Test]
        public void WhenTryAddItemToInventory_AndInventoryCapacityIsFull_TheNotAddItemToInventory()
        {
            //Arrange
            Inventory inventory = new Inventory(1, _inventoryEventNotifier);
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "Helmet", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            TestHelper.TryAddItemToInventory(inventory, itemConfig);
            
            //Act
            TestHelper.TryAddItemToInventory(inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(1, inventory.GetInventoryItems().Count);
        }
        
        
        [Test]
        public void WhenTryAddItemToInventory_AndItemNameIsEmpty_ThenNotAddItemToInventory()
        {
            var itemComponents = new List<IItemComponent>();
            
            var itemConfig = TestHelper.CreateItemConfig(
                "", 
                InventoryType.Wearable, 
                EquipmentSlotType.Head,
                itemComponents);
            
            //Act
            TestHelper.TryAddItemToInventory(_inventory, itemConfig);
            
            //Assert
            Assert.AreEqual(0, _inventory.GetInventoryItems().Count);
        }
    }
    
    public class TestCharacter : IEntity
    {
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }
        
    }

    public static class TestHelper
    {
        public static EquipmentManager CreateEquipmentManager(
            EquipmentSlot headSlot,
            EquipmentSlot bodySlot,
            EquipmentSlot armsSlot,
            EquipmentSlot feetSlot,
            Inventory inventory)
        {
            return new EquipmentManager(
                headSlot, 
                bodySlot, 
                armsSlot, 
                feetSlot,
                inventory);
        }
        public static ItemConfig CreateItemConfig(
            string itemName, 
            InventoryType inventoryType, 
            EquipmentSlotType equipmentSlotType,
            List<IItemComponent> itemComponents)
        {
            var itemConfig = ScriptableObject.CreateInstance<ItemConfig>();
            InventoryItem item = new InventoryItem()
            {
                name = itemName,
                inventoryType = inventoryType,
                equipmentSlotType = equipmentSlotType,
                itemComponents = itemComponents
            };
            itemConfig.inventoryItem = item;
            return itemConfig;
        }
        
        public static bool TryAddItemToInventory(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            if (item != null)
            {
                inventory.AddItem(item);
                return true;
            }
            return false;
        }

        public static bool TryEquipItem(EquipmentManager equipmentManager, ItemConfig config)
        {
            return equipmentManager.TryEquipItem(config.inventoryItem);
        }
        
        public static bool TryUnequipItem(EquipmentManager equipmentManager, ItemConfig config)
        {
            return equipmentManager.TryUnequipItem(config.inventoryItem);
        }

        public static bool TryRemoveItemFromInventory(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            return inventory.TryRemoveItem(item);
        }

        public static bool TryConsumeItem(Inventory inventory, ItemConfig config)
        {
            InventoryItem item = config.inventoryItem.Clone();
            return inventory.TryConsumeItem(item);
        }
    }
}

