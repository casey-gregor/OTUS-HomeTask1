using System;

namespace Inventory
{
    public sealed class SpeedComponent: IItemComponent
    {
        public int Speed = 1;
        public IItemComponent Clone()
        {
            return new SpeedComponent()
            {
                Speed = Speed
            };
        }

        public void Apply(IEntity entity)
        {
            entity.Speed += Speed;
        }

        public void Remove(IEntity entity)
        {
            entity.Speed = Math.Max(0, entity.Speed - Speed);
        }
    }
}