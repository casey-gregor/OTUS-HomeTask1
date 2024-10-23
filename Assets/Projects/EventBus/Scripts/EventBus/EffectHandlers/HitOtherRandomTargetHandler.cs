using UnityEngine;
using Zenject;

namespace EventBus
{
    public sealed class HitOtherRandomTargetHandler : IInitializable, ILateDisposable
    {
        private const float EffectChance = 0.5f;
        private readonly EventBus _eventBus;
        public HitOtherRandomTargetHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        private void HandleEffect(HitOtherRandomTargetEffect effect)
        {
            Debug.Log("In different random target effect handler");
            
            if (IsEffectTriggered())
            {
                PlayerEntity targetEntity = effect.Target.PlayerEntity;
                if (targetEntity.HeroComponent.GetAliveHeroesCount() > 1)
                {
                    HeroEntity differentTarget;
                    int maxAttempts = 10;
                    int attempts = 0;
        
                    do
                    {
                        differentTarget = targetEntity.HeroComponent.GetRandomHero();
                        attempts++;
                    } while (effect.Target == differentTarget && attempts < maxAttempts);
                    effect.Target = differentTarget;
                }
                effect.RaisedSuccessfully = true;
            }
            else
            {
                effect.RaisedSuccessfully = false;
            }
        }
        
        private bool IsEffectTriggered()
        {
            return UnityEngine.Random.value > EffectChance;
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<HitOtherRandomTargetEffect>(HandleEffect);
        }

        public void LateDispose()
        {
            _eventBus.UnsubscribeHandler<HitOtherRandomTargetEffect>(HandleEffect);
        }
    }
}