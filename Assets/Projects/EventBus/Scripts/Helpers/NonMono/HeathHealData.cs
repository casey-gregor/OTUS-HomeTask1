namespace EventBus
{
    public class HeathHealData
    {
        public PlayerEntity Source;

        public HeathHealData(PlayerEntity source)
        {
            Source = source;
        }
    }
}