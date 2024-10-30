namespace Inventory.ItemComponents
{
    public sealed class DefenceComponent : IItemComponent
    {
        public int Defence = 1;
        public IItemComponent Clone()
        {
            return new DefenceComponent()
            {
                Defence = Defence
            };
        }

        public void Apply(Entity entity)
        {
            entity.armor += Defence;
        }

        public void Remove(Entity entity)
        {
            if(entity.armor > 0)
                entity.armor -= Defence;
        }
    }
}