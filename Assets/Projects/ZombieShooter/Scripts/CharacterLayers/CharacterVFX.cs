using System;
using UnityEngine;

namespace ZombieShooter
{
    [Serializable]
    public class CharacterVFX
    {
        [SerializeField] private ParticleSystem _shootParticlePrefab;
        [SerializeField] private ParticleSystem _takeDamagePrefab;

        public void Construct(CharacterCore core)
        {
            core.ShootComponent.FireEvent.Subscribe(HandleShootActionEvent);
            core.LifeComponent.DeductHitPointEvent.Subscribe(HandleTakeDamageEvent);
        }

        private void HandleTakeDamageEvent(int obj)
        {
            _takeDamagePrefab.Play();
        }

        private void HandleShootActionEvent()
        {
            _shootParticlePrefab.Play();
        }
    }
}