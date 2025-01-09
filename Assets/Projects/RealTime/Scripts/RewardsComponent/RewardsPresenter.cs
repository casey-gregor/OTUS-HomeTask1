
using UnityEngine;

namespace RealTime.Rewards
{
    public sealed class RewardsPresenter
    {
        public void RewardInstantiated(RewardsView rewardsView, ChestPresenter chestPresenter)
        {
            foreach (IReward reward in chestPresenter.Rewards)
            {
                HandleReward(reward, rewardsView);
            }
        }
        
        private void HandleReward(IReward reward, RewardsView rewardsView)
        {
            switch (reward)
            {
                case MoneyReward moneyReward:
                    string moneyText = moneyReward.Money.ToString();
                    rewardsView.SetMoneyReward(moneyText);
                    break;

                case ResourceReward resourceReward:
                    string resourceText = resourceReward.Resource.ToString();
                    rewardsView.SetResourcesReward(resourceText);
                    break;

                default:
                    Debug.LogWarning($"Unhandled reward type: {reward.GetType().Name}");
                    break;
            }
        }
    }
}