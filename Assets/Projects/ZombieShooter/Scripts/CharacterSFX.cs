using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterSFX
    {

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _takeDamageAudio;
        [SerializeField] private AudioClip _shooteAudio;

        public void Construct(CharacterCore core)
        {
            core.ShootComponent.FireEvent.Subscribe(HandleShootActionEvent);
            core.LifeComponent.TakeDamageEvent.Subscribe(HandleTakeDamageEvent);
        }

        private void HandleShootActionEvent()
        {
            _source.PlayOneShot(_shooteAudio);
        }

        private void HandleTakeDamageEvent(int obj)
        {
            _source.PlayOneShot(_takeDamageAudio);
        }
    }
}