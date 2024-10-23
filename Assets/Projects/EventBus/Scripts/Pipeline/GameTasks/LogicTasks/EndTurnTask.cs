using UnityEngine;

namespace EventBus
{
    public sealed class EndTurnTask : GameTask
    {
        private readonly EventBus _eventBus;
        private readonly PipelineContext _context;
        private readonly VisualPipeline _visualPipeline;

        public EndTurnTask(EventBus eventBus, PipelineContext context, VisualPipeline visualPipeline)
        {
            _eventBus = eventBus;
            _context = context;
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

            int attackerHeroEntityIndex = _context.AttackerHeroEntityIndex;
            if (attackerHeroEntityIndex == -1)
            {
                Finish();
                return;
            }
            
            int attackerIndex = _context.AttackerIndex;
            _context.PlayerEntitiesDict[attackerIndex].HeroComponent.HeroEntities
                .TryGetHeroEntity(attackerHeroEntityIndex, out HeroEntity attackerHeroEntity);
            
            _visualPipeline.AddGameTask(new ToggleHeroViewTask(attackerHeroEntity.View, false));
            
            Finish();
        }
        
        private bool CheckForGameOver()
        {
            PlayerEntity attackerPlayerEntity = _context.PlayerEntitiesDict[_context.AttackerIndex];
            PlayerEntity targetPlayerEntity = _context.PlayerEntitiesDict[_context.TargetIndex];
            
            int attackerAliveCount = attackerPlayerEntity.HeroComponent.GetAliveHeroesCount();
            int targetAliveCount = targetPlayerEntity.HeroComponent.GetAliveHeroesCount();
            
            if (attackerAliveCount == 0 || targetAliveCount == 0)
            {
                var winnerPlayerEntity = attackerAliveCount == 0 ? targetPlayerEntity : attackerPlayerEntity;
                _eventBus.RaiseEvent(new GameOverEvent(winnerPlayerEntity));
                
                return true;
            }
            return false;
        }
    }
}