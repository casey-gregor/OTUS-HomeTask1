using System.Collections.Generic;

namespace RealTime
{
    public sealed class ApplyChestReward
    {
        private readonly IEntity _entity;

        public ApplyChestReward(IEntity entity)
        {
            _entity = entity;
        }

        public void ApplyReward(IReadOnlyList<IReward> rewards)
        {
            foreach (IReward reward in rewards)
            {
                reward.Apply(_entity);
            }
        }
    }
}