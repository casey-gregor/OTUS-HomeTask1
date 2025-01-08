namespace RealTime
{
    public sealed class MoneyReward : IReward
    {
        public int Money;

        public IReward Clone()
        {
            return new MoneyReward(){Money = Money};
        }

        public void Apply(IEntity entity)
        {
            entity.Money += Money;
        }
    }
}