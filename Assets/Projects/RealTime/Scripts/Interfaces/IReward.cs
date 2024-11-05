namespace RealTime
{
    public interface IReward
    {
        public IReward Clone();
        public void Apply(IEntity entity);
    }
}