using UnityEngine;

namespace EventBus
{
    public sealed class EndTurnTask : GameTask
    {
        private readonly EventBus _eventBus;
        private readonly PipelineContext _pipelineContext;
        private readonly VisualPipeline _visualPipeline;

        public EndTurnTask(EventBus eventBus, PipelineContext pipelineContext, VisualPipeline visualPipeline)
        {
            _eventBus = eventBus;
            _pipelineContext = pipelineContext;
            _visualPipeline = visualPipeline;
        }

        protected override void OnRun()
        {
            Debug.Log("EndTask run");
            
            if (CheckForGameOver())
            {
                Finish();
                return;
            }

            int attackerIndex = _pipelineContext.AttackerIndex;
            int attackerHeroEntityIndex = _pipelineContext.AttackerHeroEntityIndex;
            _pipelineContext.PlayerEntitiesDict[attackerIndex].HeroEntities
                .TryGetHeroEntity(attackerHeroEntityIndex, out HeroEntity attackerHeroEntity);
            
            _visualPipeline.AddGameTask(new ToggleHeroViewTask(attackerHeroEntity.View, false));
            
            Finish();
        }
        
        private bool CheckForGameOver()
        {
            PlayerEntity attackerPlayerEntity = _pipelineContext.PlayerEntitiesDict[_pipelineContext.AttackerIndex];
            PlayerEntity targetPlayerEntity = _pipelineContext.PlayerEntitiesDict[_pipelineContext.TargetIndex];
            
            int attackerAliveCount = attackerPlayerEntity.GetAliveHeroesCount();
            int targetAliveCount = targetPlayerEntity.GetAliveHeroesCount();
            
            if (attackerAliveCount == 0 || targetAliveCount == 0)
            {
                var winnerPlayerEntity = attackerPlayerEntity.GetAliveHeroesCount() == 0 ? targetPlayerEntity : attackerPlayerEntity;
                _eventBus.RaiseEvent(new GameOverEvent(winnerPlayerEntity));
                
                return true;
            }
            return false;
        }
    }
}