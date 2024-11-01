using System;

namespace Inventory
{
    public sealed class AttackComponent: IItemComponent
    {
        public int Attack = 1;
        public IItemComponent Clone()
        {
            return new AttackComponent()
            {
                Attack = Attack
            };
        }

        public void Apply(IEntity entity)
        {
            entity.Attack += Attack;
        }

        public void Remove(IEntity entity)
        {
            entity.Attack = Math.Max(0, entity.Attack - Attack);
        }
    }
}