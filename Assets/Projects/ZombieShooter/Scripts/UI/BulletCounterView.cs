using System.Collections.Generic;
using Atomic.Extensions;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace ZombieShooter
{
    public class BulletCounterView : MonoBehaviour
    {
        [SerializeField] private Transform bulletsParent;
        [SerializeField] private GameObject bulletImageUI;
        [SerializeField] private Character character;
        [SerializeField] private TextMeshProUGUI reloadingText;

        private readonly List<GameObject> _bulletsList = new();
        private int _bulletsCount;
        private Sequence _reloadSequence;

        private void Awake()
        {
            character.GetObservable(CharacterAPIKeys.REMOVE_BULLET).Subscribe(RemoveBullet);
            character.GetObservable(CharacterAPIKeys.ADD_BULLET).Subscribe(AddBullet);
            _bulletsCount = character.GetVariable<int>(CharacterAPIKeys.BULLET_COUNT).Value;
            for (int i = 0; i < _bulletsCount; i++)
            {
                var bullet = Instantiate(bulletImageUI, bulletsParent);
                _bulletsList.Add(bullet);
            }
            reloadingText.gameObject.SetActive(false);
            _reloadSequence = DOTween.Sequence();
            _reloadSequence
                .Append(reloadingText.DOFade(0, 0.4f)
                    .SetLoops(-1, LoopType.Yoyo))
                .SetAutoKill(false);
        }

        private void AddBullet()
        {
            _bulletsList[_bulletsCount++].SetActive(true);
            BlinkReloadingText(_bulletsCount);
        }

        private void RemoveBullet()
        {
            _bulletsList[--_bulletsCount].SetActive(false);
            BlinkReloadingText(_bulletsCount);
        }
        
        private void BlinkReloadingText(int bulletsCount)
        {
            if (bulletsCount <= 0)
            {
                reloadingText.gameObject.SetActive(true);
                _reloadSequence.Play();
            }
            else
            {
                _reloadSequence.Pause();
                reloadingText.gameObject.SetActive(false);
            }
        }
        
    }
}