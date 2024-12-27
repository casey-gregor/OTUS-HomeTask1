using System.Collections.Generic;
using Projects.Inventory.Scripts.UI;
using UnityEngine;

namespace Inventory
{
    public class UISlotProvider
    {
        private readonly UIManager _uiManager;

        public UISlotProvider(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public Transform GetFreeInventorySlot()
        {
            return GetFreeSlot(_uiManager.BackpackSlots);
        }

        public Transform GetEquipmentSlot(EquipmentSlot slot)
        {
            switch (slot.GetSlotType())
            {
                case EquipmentSlotType.Head:
                    return _uiManager.HeadSlot;
                case EquipmentSlotType.Body:
                    return _uiManager.BodySlot;
                case EquipmentSlotType.RightHand:
                    return _uiManager.RightHandSlot;
                case EquipmentSlotType.LeftHand:
                    return _uiManager.LeftHandSlot;
                case EquipmentSlotType.Feet:
                    return _uiManager.FeetSlot;
        default:
                    return GetFreeSlot(_uiManager.BackpackSlots);
            }
        }

        private Transform GetFreeSlot(IReadOnlyList<Transform> slots)
        {
            foreach (var slot in slots)
            {
                if (slot.childCount == 0)
                {
                    return slot;
                }
               
            }
            return null;
        }
    }
}