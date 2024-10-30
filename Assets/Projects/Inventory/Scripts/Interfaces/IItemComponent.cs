namespace Inventory
{
    public interface IItemComponent
    {
        public IItemComponent Clone();
        public void Apply(Entity entity);
        public void Remove(Entity entity);
    }
}