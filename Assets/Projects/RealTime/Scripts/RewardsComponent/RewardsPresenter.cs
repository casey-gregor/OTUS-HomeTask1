using System;

namespace RealTime.Rewards
{
    public class RewardsPresenter : IDisposable
    {
        private RewardsMover _rewardsMover;

        public RewardsPresenter(RewardsMover rewardsMover)
        {
            _rewardsMover = rewardsMover;
            
            _rewardsMover.OnSetRewards += SetRewards;
        }

        private void SetRewards(RewardsView rewardsView, ChestModel chestModel)
        {
            foreach (IReward reward in chestModel.Rewards)
            {
                if (reward is MoneyReward moneyReward)
                {
                    rewardsView.SetMoneyReward(moneyReward.Money.ToString());
                }
                else if (reward is ResourceReward resourceReward)
                {
                    rewardsView.SetResourcesReward(resourceReward.Resource.ToString());
                }
            }
        }

        public void Dispose()
        {
            _rewardsMover.OnSetRewards -= SetRewards;
        }
    }
}