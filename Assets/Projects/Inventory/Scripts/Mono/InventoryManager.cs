using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Inventory
{
    public sealed class InventoryManager : MonoBehaviour
    {
        public Entity entity;
        public ItemConfig config;
        public ItemView itemPrefab;
        public int InventorySlotsNum => _inventorySlotsNum;
        
        [Min(-1)]
        public int inventorySlotCapacity = -1;// -1 is unlimited
        [Min(1)]
        public int headSlotCapacity = 1;
        [Min(1)]
        public int bodySlotCapacity = 1;
        [Min(1)]
        public int rightHandSlotCapacity = 1;
        [Min(1)]
        public int leftHandSlotCapacity = 1;
        [Min(1)]
        public int feetSlotCapacity = 1;
        
        private const int _inventorySlotsNum = 12;
        public Inventory Inventory { get; private set; }
        public EquipmentManager EquipmentManager { get; private set; }
        
        private ItemInstantiator _itemInstantiator;
        
        [Inject]
        private void Construct(Inventory inventory, EquipmentManager equipmentManager)
        {
            Inventory = inventory;
            EquipmentManager = equipmentManager;
        }

        [Button]
        public void AddItemToInventory()
        {
            InventoryItem item = config.inventoryItem.Clone();
            if (item != null)
            {
                Inventory.AddItem(item);
            }
        }
        
        [Button]
        public void RemoveItemFromInventory()
        {
            Inventory.RemoveItemCompletely(config.inventoryItem);
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