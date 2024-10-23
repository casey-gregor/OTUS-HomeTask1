using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class ToggleHeroViewTask : GameTask
    {
        private readonly HeroView _currentAttackHero;
        private readonly bool _isActive;

        public ToggleHeroViewTask(HeroView currentAttackHero, bool isActive)
        {
            _currentAttackHero = currentAttackHero;
            _isActive = isActive;
        }
        protected override void OnRun()
        {
            Debug.Log("ToggleHeroViewTask run");
            _currentAttackHero.SetActive(_isActive);
            
            Finish();
        }
    }
}