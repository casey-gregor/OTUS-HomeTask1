namespace Inventory
{
    public sealed class SlotsManager
    {
        private readonly InventorySlot _headSlot;
        private readonly InventorySlot _bodySlot;
        private readonly InventorySlot _armsSlot;
        private readonly InventorySlot _feetSlot;
        private readonly InventorySlot _backpackSlot;
        

        public SlotsManager(
            InventorySlot headSlot, 
            InventorySlot bodySlot, 
            InventorySlot armsSlot, 
            InventorySlot feetSlot, 
            InventorySlot backpackSlot)
        {
            _headSlot = headSlot;
            _bodySlot = bodySlot;
            _armsSlot = armsSlot;
            _feetSlot = feetSlot;
            _backpackSlot = backpackSlot;
        }

        public bool CheckIfConsumable(InventoryItem item)
        {
            return _backpackSlot.CheckIfConsumable(item);
        }
        
        public InventorySlot TryAddItem(InventoryItem item)
        {
            var slot = GetSlot(item.slotType);
            if (slot.TryAddItem(item))
            {
                return slot;
            }

            if(_backpackSlot.Capacity == -1 && _backpackSlot.TryAddItem(item))
            {
                slot = _backpackSlot;
                return slot;
            }

            return null;
        }

        public InventorySlot TryRemoveItem(InventoryItem item)
        {
            if (_backpackSlot.TryRemoveItem(item))
            {
                return _backpackSlot;
            }
            
            var slot = GetSlot(item.slotType);
            if (slot.TryRemoveItem(item))
            {
                return slot;
            }
            return null;
        }

        public InventorySlot GetSlot(SlotType slotType)
        {
            return slotType switch
            {
                SlotType.Head => _headSlot,
                SlotType.Body => _bodySlot,
                SlotType.Arms => _armsSlot,
                SlotType.Feet => _feetSlot,
                SlotType.Backpack => _backpackSlot,
                _ => null
            };
        }
        
        public bool GetSlot(SlotType slotType, out InventorySlot slot)
        {
            switch (slotType)
            {
                case SlotType.Head:
                    slot = _headSlot;
                    return true;
                case SlotType.Body:
                    slot = _bodySlot;
                    return true;
                case SlotType.Arms:
                    slot = _armsSlot;
                    return true;
                case SlotType.Feet:
                    slot = _feetSlot;
                    return true;
                case SlotType.Backpack:
                    slot = _backpackSlot;
                    return true;
                default:
                    slot = null;
                    return false;
            }
        }
    }
}