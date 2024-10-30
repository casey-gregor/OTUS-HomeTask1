using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string name;
        public Sprite icon;
        public InventoryType inventoryType;
        public SlotType slotType;
        
        [SerializeReference] public List<IItemComponent> itemComponents;
       
        public InventoryItem Clone()
        {
            return new InventoryItem()
            {
                name = name,
                icon = icon,
                inventoryType = inventoryType,
                slotType = slotType,
                itemComponents = CloneComponents()
            };
        }

        private List<IItemComponent> CloneComponents()
        {
            List<IItemComponent> components = new List<IItemComponent>();
            foreach (var component in itemComponents)
            {
                component.Clone();
                components.Add(component);
            }

            return components;
        }
    }
}