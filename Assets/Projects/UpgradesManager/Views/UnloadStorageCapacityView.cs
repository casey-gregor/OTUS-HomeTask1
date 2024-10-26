
namespace UpgradesManager
{
    public class UnloadStorageCapacityView : View
    {
        public override void UpdateCapacity(int value)
        {
            string capacityText = $"Unload Storage Capacity : {value}";
            capacity.text = capacityText;
        }
    }
}