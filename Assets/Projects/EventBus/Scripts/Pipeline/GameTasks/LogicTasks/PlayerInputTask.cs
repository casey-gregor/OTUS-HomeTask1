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
        private readonly PipelineContext _pipelineContext;
        private readonly PlayerEntitiesInitiator _playerEntitiesInitiator;

        public PlayerInputTask(EventBus eventBus, PlayerEntitiesInitiator playerEntitiesInitiator, PipelineContext pipelineContext)
        {
            _eventBus = eventBus;
            _playerEntitiesInitiator = playerEntitiesInitiator;
            _pipelineContext = pipelineContext;
        }
        
        protected override void OnRun()
        {
            Debug.Log("PlayerInputTask OnRun");

            int attackerIndex = _pipelineContext.AttackerIndex;
            int targetEntityIndex = (attackerIndex + 1) % _pipelineContext.NumberOfPlayers;
            _pipelineContext.SetTargetIndex(targetEntityIndex);
            _targetPlayerEntity = _pipelineContext.PlayerEntitiesDict[targetEntityIndex];
            
            _targetPlayerView = _targetPlayerEntity.PlayerView;
            
            _targetPlayerView.OnHeroClicked += CheckIfChosenHeroAlive;
            
        }

        private void CheckIfChosenHeroAlive(HeroView targetHeroView)
        {
            List<HeroView> heroViews = _targetPlayerEntity.HeroViews.ToList();
            int targetHeroViewIndex = heroViews.IndexOf(targetHeroView);
            
            _targetPlayerEntity.HeroEntities.TryGetHeroEntity(targetHeroViewIndex, out HeroEntity targetHeroEntity);

            if (!targetHeroEntity.IsDead)
            {
                int currentAttackerIndex = _pipelineContext.AttackerIndex;
                int currentHeroEntityIndex = _pipelineContext.AttackerHeroEntityIndex;
            
                PlayerEntity attacker = _pipelineContext.PlayerEntitiesDict[currentAttackerIndex];
                attacker.HeroEntities.TryGetHeroEntity(currentHeroEntityIndex, out HeroEntity attackerHeroEntity);
                RaiseEvent(attackerHeroEntity, targetHeroEntity);
                return;
            }
        }

        private void RaiseEvent(HeroEntity attackerHeroEntity, HeroEntity targetHeroEntity)
        {
            _eventBus.RaiseEvent(new PreAttackCheckEvent(attackerHeroEntity, targetHeroEntity));

            Finish();
        }

        protected override void OnFinish()
        {
            if(_targetPlayerView != null)
                _targetPlayerView.OnHeroClicked -= CheckIfChosenHeroAlive;
        }
    }
}