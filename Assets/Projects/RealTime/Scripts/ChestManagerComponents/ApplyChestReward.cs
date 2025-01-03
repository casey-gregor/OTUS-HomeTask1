using System.Collections.Generic;

namespace RealTime
{
    public class ApplyChestReward
    {
        private IEntity _entity;

        public ApplyChestReward(IEntity entity)
        {
            _entity = entity;
        }

        public void ApplyReward(List<IReward> rewards)
        {
            foreach (IReward reward in rewards)
            {
                reward.Apply(_entity);
            }
        }
    }
}