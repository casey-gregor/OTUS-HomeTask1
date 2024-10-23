using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class PlayerInputTask : GameTask
    {
        private PlayerEntity _targetPlayerEntity;
        private HeroListView _targetPlayerView;
        
        private readonly EventBus _eventBus;
        private readonly PipelineContext _context;

        public PlayerInputTask(EventBus eventBus, PipelineContext context)
        {
            _eventBus = eventBus;
            _context = context;
        }
        
        protected override void OnRun()
        {
            Debug.Log("PlayerInputTask started");
            int attackerHeroEntityIndex = _context.AttackerHeroEntityIndex;
  
            if (attackerHeroEntityIndex == -1)
            {
                Finish();
                return;
            }
            
            int attackerIndex = _context.AttackerIndex;
            int targetEntityIndex = (attackerIndex + 1) % _context.NumberOfPlayers;
            _context.SetTargetIndex(targetEntityIndex);
            _targetPlayerEntity = _context.PlayerEntitiesDict[targetEntityIndex];
            
            _targetPlayerView = _targetPlayerEntity.HeroComponent.PlayerView;
            
            _targetPlayerView.OnHeroClicked += CheckIfChosenHeroAlive;
        }

        private void CheckIfChosenHeroAlive(HeroView targetHeroView)
        {
            List<HeroView> heroViews = _targetPlayerEntity.HeroComponent.HeroViews.ToList();
            int targetHeroViewIndex = heroViews.IndexOf(targetHeroView);
            
            _targetPlayerEntity.HeroComponent.HeroEntities.TryGetHeroEntity
                (targetHeroViewIndex, out HeroEntity targetHeroEntity);

            if (!targetHeroEntity.HealthComponent.IsDead)
            {
                int currentAttackerIndex = _context.AttackerIndex;
                int currentHeroEntityIndex = _context.AttackerHeroEntityIndex;
            
                PlayerEntity attacker = _context.PlayerEntitiesDict[currentAttackerIndex];
                attacker.HeroComponent.HeroEntities.TryGetHeroEntity
                    (currentHeroEntityIndex, out HeroEntity attackerHeroEntity);
                
                _eventBus.RaiseEvent(new CheckAttackerEffectsEvent(attackerHeroEntity, targetHeroEntity, EffectType.Offensive));
                
                Finish();
            }
        }

        protected override void OnFinish()
        {
            if(_targetPlayerView != null)
                _targetPlayerView.OnHeroClicked -= CheckIfChosenHeroAlive;
        }
    }
}