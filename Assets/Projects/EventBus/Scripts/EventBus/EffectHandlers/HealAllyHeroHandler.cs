using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class HealAllyHeroHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly ExtraPipeline _extraPipeline;

        public HealAllyHeroHandler(EventBus eventBus, ExtraPipeline extraPipeline)
        {
            _eventBus = eventBus;
            _extraPipeline = extraPipeline;
        }

        private void HandleEffect(HealAllyHeroEffect effect)
        {
            Debug.Log("in HealAllyHeroes EffectHandler");
 
            PlayerEntity attackerEntity = effect.Source.PlayerEntity;

            HeroEntity heroToHeal = GetHeroToHeal(attackerEntity, effect.Source);
            effect.Ally = heroToHeal;
            HealEvent healEvent = new HealEvent(effect.Source, heroToHeal, effect.HealAmount);
            _extraPipeline.AddGameTask(new PostAttackTask<HealEvent>(_eventBus,healEvent));
            effect.RaisedSuccessfully = true;
            // _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
        }

        private HeroEntity GetHeroToHeal(PlayerEntity attackerEntity, HeroEntity source)
        {
            if (attackerEntity.HeroComponent.GetAliveHeroesCount() > 1)
            {
                HeroEntity heroToHeal;
                do
                {
                    heroToHeal = attackerEntity.HeroComponent.GetRandomHero();
                } while (heroToHeal == source);
                return heroToHeal;
            }
            return source;
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<HealAllyHeroEffect>(HandleEffect);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<HealAllyHeroEffect>(HandleEffect);
        }
    }
}