
namespace UpgradesManager
{
    public sealed class LoadStorageCapacityView : View
    {
        public override void UpdateCapacity(int value)
        {
            string capacityText = $"Load Storage Capacity : {value}";
            capacity.text = capacityText;
        }
    }
}