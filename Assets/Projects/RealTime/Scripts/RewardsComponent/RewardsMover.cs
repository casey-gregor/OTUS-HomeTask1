using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime.Rewards
{
    public class RewardsMover : IDisposable
    {
        public event Action<RewardsView, ChestModel> OnSetRewards;
        private readonly ChestButtonTracker _chestButtonTracker;
        private readonly ChestLocker _chestLocker;
        private readonly RewardsView _rewardsView;

        private readonly Dictionary<ChestModel, RewardsView> _rewards = new();

        public RewardsMover(
            RewardsView rewardsView, 
            ChestButtonTracker chestButtonTracker, 
            ChestLocker chestLocker)
        {
            _rewardsView = rewardsView;
            _chestButtonTracker = chestButtonTracker;
            _chestLocker = chestLocker;
            
            _chestButtonTracker.OnChestButtonPressed += ShowRewards;
            _chestLocker.OnChestLocked += RunRewards;
        }
        
        public void Dispose()
        {
            _chestButtonTracker.OnChestButtonPressed -= ShowRewards;
        }

        private void ShowRewards(ChestModel chestModel)
        {
            ShakeChest(chestModel.GetChestView().ImageComponent, ()=> PopupSequence(_rewardsView, chestModel));
        }
        private void RunRewards(ChestModel chestModel)
        {
            RewardsView rewardView = _rewards[chestModel];
            ChestView chestView = chestModel.GetChestView();
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(rewardView.transform.DOLocalMoveY(chestView.transform.localPosition.y + 140, 0.5f).SetEase(Ease.OutBack))
                .Append(rewardView.transform.DOLocalMoveY(chestView.transform.localPosition.y,0.3f))
                .Join(rewardView.transform.DOScale(0, 0.5f))
                .OnComplete(()=> GameObject.Destroy(rewardView.gameObject))
                .Play();
            _rewards.Remove(chestModel);
        }

        private void PopupSequence(RewardsView rewardsPopupPrefab, ChestModel chestModel)
        {
            ChestView chestView = chestModel.GetChestView();
            RewardsView rewardView = GameObject.Instantiate(rewardsPopupPrefab, chestView.transform);
            _rewards.Add(chestModel, rewardView);
            
            OnSetRewards?.Invoke(rewardView, chestModel);
            
            chestView.OpenEffect.Play();
            float particleDelay = chestView.OpenEffect.main.duration * 0.8f;
            
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendInterval(particleDelay)
                .AppendCallback(()=> SwitchOnPopup(rewardView, chestView))
                .Append(rewardView.transform.DOScale(1, 0.5f))
                .Join(rewardView.transform.DOLocalMoveY(chestView.transform.localPosition.y + 140, 0.7f).SetEase(Ease.OutBack))
                .Append(rewardView.transform.DOLocalMoveY(chestView.transform.localPosition.y,0.5f).SetEase(Ease.OutBounce))
                .Play();
        }

        private void SwitchOnPopup(RewardsView rewardsPopup, ChestView chestView)
        {
            rewardsPopup.gameObject.SetActive(true);
            rewardsPopup.transform.localPosition = chestView.transform.localPosition;
            rewardsPopup.transform.localScale = Vector3.zero;
        }

        private void ShakeChest(Image chestImage, Action popupSequence)
        {
            var rectTransform = chestImage.rectTransform;
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(rectTransform.DOShakeAnchorPos(
                    duration: 0.2f,   
                    strength: 20f,
                    vibrato: 10,
                    randomness: 90,
                    snapping: false,
                    fadeOut: true
                ))
                .Join(rectTransform.DOPunchAnchorPos(
                    punch: new Vector2(0, -50),
                    duration: 0.3f, 
                    vibrato: 20,
                    elasticity: 0.9f 
                ).SetEase(Ease.InOutQuad))
                .OnComplete(()=> popupSequence())
                .Play();
            
        }
    }
}