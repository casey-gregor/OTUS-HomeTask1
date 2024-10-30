namespace Inventory.ItemComponents
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

        public void Apply(Entity entity)
        {
            entity.speed += Speed;
        }

        public void Remove(Entity entity)
        {
            if(entity.speed > 0)
                entity.speed -= Speed;
        }
    }
}