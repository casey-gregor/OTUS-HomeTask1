namespace Inventory
{
    public interface IEntity
    {
        int Health { get; set; }
        int Armor { get; set; }
        int Attack { get; set; }
        int Speed { get; set; }
    }
}