﻿using Atomic.Extensions;
using Atomic.Objects;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Character _character;


        private void Awake()
        {
            var isDeadObservable = _character.GetObservable<bool>(APIKeys.IS_DEAD);
            isDeadObservable.Subscribe(value =>
            {
                isDeadObservable.Unsubscribe(HandleCharacterDead);
                if (value)
                    HandleCharacterDead(value);
            });
        }
        private void Update()
        {
            
        }

        private void HandleCharacterDead(bool value)
        {
            
        }

    }
}