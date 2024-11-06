using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string name;
        public Sprite icon;
        public InventoryType inventoryType;
        public EquipmentSlotType equipmentSlotType;
        
        [SerializeReference] public List<IItemComponent> itemComponents;
       
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
    }
}