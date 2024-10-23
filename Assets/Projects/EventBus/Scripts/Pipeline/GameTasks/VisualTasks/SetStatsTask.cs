using UnityEngine;

namespace EventBus
{
    public sealed class SetStatsTask : GameTask
    {
        private readonly HeroEntity _attackerHeroEntity;
        private readonly HeroEntity _targetHeroEntity;

        public SetStatsTask(HeroEntity attackerHeroEntity = default, HeroEntity targetHeroEntity = default)
        {
            _attackerHeroEntity = attackerHeroEntity;
            _targetHeroEntity = targetHeroEntity;
        }

        protected override void OnRun()
        {
            Debug.Log("In SetStatsTask");
            
            if(_attackerHeroEntity != null)
                _attackerHeroEntity.UIComponent.SetStats();
            
            if(_targetHeroEntity != null)
                _targetHeroEntity.UIComponent.SetStats();

            Finish();
        }
    }
}