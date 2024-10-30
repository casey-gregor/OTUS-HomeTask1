
namespace Inventory
{
    public sealed class HealthComponent: IItemComponent
    {
        public int Health = 1;
        public IItemComponent Clone()
        {
            return new HealthComponent()
            {
                Health = Health
            };
        }

        public void Apply(Entity entity)
        {
            entity.health += Health;
        }

        public void Remove(Entity entity)
        {
            if(entity.health > 0)
                entity.health -= Health;
        }
    }
}