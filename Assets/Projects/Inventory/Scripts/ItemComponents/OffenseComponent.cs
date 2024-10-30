namespace Inventory
{
    public sealed class OffenseComponent: IItemComponent
    {
        public int Offence = 1;
        public IItemComponent Clone()
        {
            return new OffenseComponent()
            {
                Offence = Offence
            };
        }

        public void Apply(Entity entity)
        {
            entity.attack += Offence;
        }

        public void Remove(Entity entity)
        {
            if(entity.attack > 0)
                entity.attack -= Offence;
        }
    }
}