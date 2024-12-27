using System;

namespace Inventory
{
    public sealed class ArmorComponent : IItemComponent
    {
        public int Armor = 1;
        public IItemComponent Clone()
        {
            return new ArmorComponent()
            {
                Armor = Armor
            };
        }

        public void Apply(IEntity entity)
        {
            entity.Armor += Armor;
        }

        public void Remove(IEntity entity)
        {
            entity.Armor = Math.Max(0, entity.Armor - Armor);
        }
    }
}