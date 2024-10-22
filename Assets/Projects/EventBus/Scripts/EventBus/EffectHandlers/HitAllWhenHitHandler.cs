using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class HitAllWhenHitHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;
        private readonly ExtraPipeline _extraPipeline;

        public HitAllWhenHitHandler(EventBus eventBus, VisualPipeline visualPipeline, ExtraPipeline extraPipeline)
        {
            _eventBus = eventBus;
            _visualPipeline = visualPipeline;
            _extraPipeline = extraPipeline;
        }

        private void HandleEffect(HitAllWhenHitEffect effect)
        {
            Debug.Log("In DamageAllWhenHitHandler");

            if (effect.Source.TryGetEffect(out IEffect foundeffect) && foundeffect == effect)
            {
                Debug.Log("Executing DamageAllWhenHitHandler for : " + effect.Source);
                PlayerEntity targetPlayer = effect.Target.PlayerEntity;
                foreach (HeroEntity heroEntity in targetPlayer.HeroEntities.GetAliveHeroes())
                {
                    if (heroEntity != effect.Target)
                    {
                        var dealDamageEvent = new DealDamageEvent(null, heroEntity, effect.ExtraDamage);
                        _extraPipeline.AddGameTask(new PostAttackTask<DealDamageEvent>(_eventBus, dealDamageEvent));
          
                    }
                }
                _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect, effect.Target, effect.Source));
                Debug.Log("stored effect all damage");
            }
            else
            {
                Debug.Log("NOT Executing DamageAllWhenHitHandler for : " + effect.Target.View.name);
            }
            
        }
        public void Initialize()
        {
            _eventBus.SubscribeHandler<HitAllWhenHitEffect>(HandleEffect);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<HitAllWhenHitEffect>(HandleEffect);
        }
    }
}