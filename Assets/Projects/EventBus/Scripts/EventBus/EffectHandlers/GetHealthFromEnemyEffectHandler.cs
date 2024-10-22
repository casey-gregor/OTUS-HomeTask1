using System;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class GetHealthFromEnemyEffectHandler : IInitializable, IDisposable
    {
        private const float EffectChance = 0f;
        private readonly EventBus _eventBus;
        private readonly ExtraPipeline _extraPipeline;
        public GetHealthFromEnemyEffectHandler(EventBus eventBus, ExtraPipeline extraPipeline)
        {
            _eventBus = eventBus;
            _extraPipeline = extraPipeline;
        }

        private void HandleEffect(GetHealthFromEnemyEffect effect)
        {
            Debug.Log("In GetHealthFromEnemy effect handler");

            if (!effect.Source.TryGetEffect(out IEffect sourceEffect) || sourceEffect != effect)
            {
                return;
            }
            
            if (IsEffectTriggered())
            {
                int healthToAdd = effect.Source.AttackDamage;
                Debug.Log("health to add : " + healthToAdd);
                HealEvent healEvent = new HealEvent(effect.Source, effect.Source, healthToAdd);
                _extraPipeline.AddGameTask(new PostAttackTask<HealEvent>(_eventBus,healEvent));
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect, effect.Source, effect.Target));
            }
        }

        private bool IsEffectTriggered()
        {
            return UnityEngine.Random.value > EffectChance;
        }

        public void Initialize()
        {
            _eventBus.SubscribeHandler<GetHealthFromEnemyEffect>(HandleEffect);
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeHandler<GetHealthFromEnemyEffect>(HandleEffect);
        }
    }
}