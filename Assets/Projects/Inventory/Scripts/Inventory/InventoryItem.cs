using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public sealed class InventoryItem : IDisposable
    {
        public string name;
        public Sprite icon;
        public InventoryType inventoryType;
        public EquipmentSlotType equipmentSlotType;
        public IReadOnlyList<IItemComponent> EffectComponents => itemComponents;
        
        [SerializeReference] private List<IItemComponent> itemComponents;
       
        private ItemView _itemView;

        public InventoryItem()
        {
            
        }
        public InventoryItem(
            string name, 
            InventoryType inventoryType,
            EquipmentSlotType equipmentSlotType,
            List<IItemComponent> itemComponents,
            Sprite icon = null)
        {
            this.name = name;
            this.inventoryType = inventoryType;
            this.equipmentSlotType = equipmentSlotType;
            this.itemComponents = itemComponents;
            this.icon = icon;
        }
        

        public void AddItemView(ItemView view)
        {
            _itemView = view;
        }

        public ItemView GetItemView()
        {
            return _itemView;
        }

        public void RemoveItemView()
        {
            if (_itemView != null)
            {
                GameObject.Destroy(_itemView.gameObject);
                _itemView = null;
            }
        }
        public InventoryItem Clone()
        {
            if (name == "")
            {
                Debug.LogWarning("Item name is empty");
                return null;
            }
            return new InventoryItem()
            {
                name = name,
                icon = icon,
                inventoryType = inventoryType,
                equipmentSlotType = equipmentSlotType,
                itemComponents = CloneComponents()
            };
        }

        private List<IItemComponent> CloneComponents()
        {
            List<IItemComponent> components = new List<IItemComponent>();
            foreach (var component in itemComponents)
            {
                if(component == null)
                    continue;
                component.Clone();
                components.Add(component);
            }

            return components;
        }

        public void Dispose()
        {
            RemoveItemView();
        }
    }
}