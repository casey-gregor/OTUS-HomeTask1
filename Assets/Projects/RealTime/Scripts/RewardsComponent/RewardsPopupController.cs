using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime.Rewards
{
    public sealed class RewardsPopupController
    {
        public event Action<RewardsView, ChestPresenter> OnRewardInstantiated;
        public IReadOnlyDictionary<RewardsView, ChestPresenter> RewardsDictionary => _rewardsDictionary;
        
        private readonly RewardsView _rewardsView;
        private readonly Dictionary<RewardsView, ChestPresenter> _rewardsDictionary = new();

        public RewardsPopupController(RewardsView rewardsView)
        {
            _rewardsView = rewardsView;
        }
        
        public void ShowRewards(ChestPresenter chestPresenter)
        {
            ShakeChest(chestPresenter.ImageComponent, ()=> PopupSequence(_rewardsView,chestPresenter));
        }
        
        public void CloseRewards(RewardsView rewardsView)
        {
            ChestPresenter chestPresenter = _rewardsDictionary[rewardsView];
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(rewardsView.transform.DOLocalMoveY(chestPresenter.ViewTransform.localPosition.y + 140, 0.5f).SetEase(Ease.OutBack))
                .Append(rewardsView.transform.DOLocalMoveY(chestPresenter.ViewTransform.transform.localPosition.y,0.3f))
                .Join(rewardsView.transform.DOScale(0, 0.5f))
                .OnComplete(()=> GameObject.Destroy(rewardsView.gameObject))
                .Play();
            _rewardsDictionary.Remove(rewardsView);
        }

        private void PopupSequence(RewardsView rewardsPopupPrefab, ChestPresenter chestPresenter)
        {
            RewardsView rewardView = GameObject.Instantiate(rewardsPopupPrefab, chestPresenter.ViewTransform.transform);
            _rewardsDictionary.Add(rewardView, chestPresenter);
            
            OnRewardInstantiated?.Invoke(rewardView, chestPresenter);
            
            chestPresenter.OpenEffect.Play();
            float particleDelay = chestPresenter.OpenEffect.main.duration * 0.8f;
            
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendInterval(particleDelay)
                .AppendCallback(()=> SwitchOnPopup(rewardView, chestPresenter.ViewTransform))
                .Append(rewardView.transform.DOScale(1, 0.5f))
                .Join(rewardView.transform.DOLocalMoveY(chestPresenter.ViewTransform.localPosition.y + 140, 0.7f).SetEase(Ease.OutBack))
                .Append(rewardView.transform.DOLocalMoveY(chestPresenter.ViewTransform.localPosition.y,0.5f).SetEase(Ease.OutBounce))
                .Play();
        }

        private void SwitchOnPopup(RewardsView rewardsPopup, Transform viewTreansform)
        {
            rewardsPopup.gameObject.SetActive(true);
            rewardsPopup.transform.position = viewTreansform.position;
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