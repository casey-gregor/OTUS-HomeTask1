﻿using System;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public class ExtraRandomHitHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly ExtraPipeline _extraPipeline;

        public ExtraRandomHitHandler(EventBus eventBus, ExtraPipeline extraPipeline)
        {
            _eventBus = eventBus;
            _extraPipeline = extraPipeline;
        }
        
        private void HandleEffect(ExtraRandomHitEffect effect)
        {
            Debug.Log("In extra hit effect handler");
            
            HeroEntity hitNewTarget = ExecuteExtraHit(effect);
            if (hitNewTarget != null)
            {
                effect.RandomTarget = hitNewTarget;
                effect.RaisedSuccessfully = true;
                // _eventBus.RaiseEvent(new StoreEffectsDataEvent(effect));
            }
            else
            {
                effect.RaisedSuccessfully = false;
            }
            
        }
        
        private HeroEntity ExecuteExtraHit(ExtraRandomHitEffect effect)
        {
            PlayerEntity targetEntity = effect.Target.PlayerEntity;
            HeroEntity newTargetEntity = effect.Target;
            if (targetEntity.HeroComponent.GetAliveHeroesCount() > 1)
            {
                int maxAttempts = 10;
                int attempts = 0;
        
                do
                {
                    newTargetEntity = targetEntity.HeroComponent.GetRandomHero();
                    attempts++;
                   
                } while (effect.Target == newTargetEntity && attempts < maxAttempts);
            }
            var checkTargetEffectEvent = new CheckTargetEffectsEvent(effect.Source, newTargetEntity, EffectType.Any);
            _extraPipeline.AddGameTask(new PostAttackTask<CheckTargetEffectsEvent>(_eventBus, checkTargetEffectEvent));
            
            var attackEvent = new AttackEvent(effect.Source, newTargetEntity, effect.ExtraDamage);
            _extraPipeline.AddGameTask(new PostAttackTask<AttackEvent>(_eventBus, attackEvent));

            return newTargetEntity;
        }
        
        public void Initialize()
        {
            _eventBus.SubscribeHandler<ExtraRandomHitEffect>(HandleEffect);
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeHandler<ExtraRandomHitEffect>(HandleEffect);
        }
        
        
        
    }
}