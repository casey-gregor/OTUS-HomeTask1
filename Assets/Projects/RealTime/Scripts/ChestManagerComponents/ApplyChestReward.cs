namespace RealTime
{
    public class ApplyChestReward
    {
        private IEntity _entity;

        public ApplyChestReward(IEntity entity)
        {
            _entity = entity;
        }

        public void ApplyReward(Chest chest)
        {
            foreach (IReward reward in chest.Rewards)
            {
                reward.Apply(_entity);
            }
        }
    }
}