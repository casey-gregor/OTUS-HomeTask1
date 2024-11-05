namespace RealTime
{
    public sealed class ResourceReward : IReward
    {
        public int Resource;
        public IReward Clone()
        {
            return new ResourceReward { Resource = Resource };
        }

        public void Apply(IEntity entity)
        {
            entity.Resource += Resource;
        }
    }
}