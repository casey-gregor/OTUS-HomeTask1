namespace Inventory
{
    public interface IItemComponent
    {
        public IItemComponent Clone();
        public void Apply(IEntity entity);
        public void Remove(IEntity entity);
    }
}