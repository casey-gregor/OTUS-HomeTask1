
namespace UpgradesManager
{
    public sealed class ProcessingTimeView : View
    {
        public override void UpdateCapacity(int value)
        {
            string capacityText = $"Processing Time Capacity : {value}";
            capacity.text = capacityText;
        }
    }
}