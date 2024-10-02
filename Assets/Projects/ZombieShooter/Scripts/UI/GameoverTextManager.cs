﻿using Atomic.Extensions;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public class GameoverTextManager : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private TextMeshProUGUI _gameOverText;

        private void Awake()
        {
            _gameOverText = GetComponentInChildren<TextMeshProUGUI>();

            var isDeadObservable = _character.GetObservable<bool>(CharacterAPIKeys.IS_DEAD);
            isDeadObservable.Subscribe(value =>
            {
                isDeadObservable.Unsubscribe(HandleCharacterDead);
                if (value)
                    HandleCharacterDead(value);
            });
        }

        private void HandleCharacterDead(bool value)
        {
            _gameOverText.enabled = value;
        }
    }
}