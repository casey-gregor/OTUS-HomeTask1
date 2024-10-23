using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class DamageAllWhenHitHandler : IInitializable, ILateDisposable
    {
        private readonly EventBus _eventBus;
        private readonly VisualPipeline _visualPipeline;
        private readonly ExtraPipeline _extraPipeline;

        public DamageAllWhenHitHandler(EventBus eventBus, VisualPipeline visualPipeline, ExtraPipeline extraPipeline)
        {
            _eventBus = eventBus;
            _visualPipeline = visualPipeline;
            _extraPipeline = extraPipeline;
        }

        private void HandleEffect(DamageaAllWhenHitEffect effect)
        {
            Debug.Log("In DamageAllWhenHitHandler");
            PlayerEntity targetPlayer = effect.Target.PlayerEntity;
            foreach (HeroEntity heroEntity in targetPlayer.HeroComponent.HeroEntities.GetAliveHeroes())
            {
                if (heroEntity != effect.Target)
                {
                    var dealDamageEvent = new DealDamageEvent(null, heroEntity, effect.ExtraDamage);
                    _extraPipeline.AddGameTask(new PostAttackTask<DealDamageEvent>(_eventBus, dealDamageEvent));
          
                }
            }
            effect.RaisedSuccessfully = true;
            // _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
            
        }
        public void Initialize()
        {
            _eventBus.SubscribeHandler<DamageaAllWhenHitEffect>(HandleEffect);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<DamageaAllWhenHitEffect>(HandleEffect);
        }
    }
}