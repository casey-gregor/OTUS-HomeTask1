using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class PlayerHealthIcons : IGameStartListener
    {
        private readonly GameObject _heartIconPrefab;
        private readonly Transform _parent;
        private readonly PlayerHitPointsComponent _playerHitPointsComponent;
        private readonly List<GameObject> _heartIcons = new();

        public PlayerHealthIcons(
            GameObject heartIconPrefab, 
            Transform parent,
            PlayerHitPointsComponent playerHitPointsComponent)
        {
            _heartIconPrefab = heartIconPrefab;
            _parent = parent;
            _playerHitPointsComponent = playerHitPointsComponent;

            _playerHitPointsComponent.hpChangedEvent += ChangeHeartIcons;
        }

        private void ChangeHeartIcons(int hitPoints)
        {
            int index = hitPoints;
            GameObject heartIcon = _heartIcons[index];
            heartIcon.SetActive(false);
        }

        public void OnStart()
        {
            for (int i = 0; i < _playerHitPointsComponent.GetHitPoints(); i++)
            {
                Debug.Log("instantiate");
                GameObject heartIcon = GameObject.Instantiate(_heartIconPrefab, _parent);
                _heartIcons.Add(heartIcon);
            }
        }
    }
}