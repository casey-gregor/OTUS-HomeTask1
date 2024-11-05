namespace RealTime
{
    public sealed class ChestDataFactory
    {
        public ChestData CreateChestData(Chest chest)
        {
            string receivedTime = TextFormatter.DateTimeToString(chest.ReceivedTime);
            string timeToOpen = TextFormatter.DateTimeToString(chest.TimeToOpen);
            return new ChestData(
                chest.ChestId, 
                receivedTime, 
                timeToOpen,
                chest.TimerMinutes.Minutes,
                chest.IsUnlocked);;
        }
    }
}