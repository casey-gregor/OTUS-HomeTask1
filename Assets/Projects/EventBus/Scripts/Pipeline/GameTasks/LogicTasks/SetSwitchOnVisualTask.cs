using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class SetSwitchOnVisualTask : GameTask
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly PipelineContext _context;
        public SetSwitchOnVisualTask(VisualPipeline visualPipeline, PipelineContext context)
        {
            _visualPipeline = visualPipeline;
            _context = context;
        }

        protected override void OnRun()
        {
            Debug.Log("SetSwitchOnVisualTask started");

            int attackerHeroEntityIndex = _context.AttackerHeroEntityIndex;
            if (attackerHeroEntityIndex == -1)
            {
                Finish();
                return;
            }
            
            int attackerIndex = _context.AttackerIndex;
            PlayerEntity player = _context.PlayerEntitiesDict[attackerIndex];
            
            player.HeroComponent.HeroEntities.TryGetHeroView
                (attackerHeroEntityIndex, out HeroView attackerHeroView);
            _visualPipeline.AddGameTask(new ToggleHeroViewTask(attackerHeroView, true));

            Finish();
        }
    }
}