using System.Collections.Generic;
using UI;
using UnityEngine;

namespace EventBus
{
    public sealed class SetSwitchOnVisualTask : GameTask
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly EventBus _eventBus;
        private readonly PipelineContext _pipelineContext;
        public SetSwitchOnVisualTask(VisualPipeline visualPipeline, EventBus eventBus, PipelineContext pipelineContext)
        {
            _visualPipeline = visualPipeline;
            _eventBus = eventBus;
            _pipelineContext = pipelineContext;
        }

        protected override void OnRun()
        {
            Debug.Log("SetSwitchOnVisualTask started");

            int currentAttackerIndex = _pipelineContext.AttackerIndex;
            PlayerEntity currentPlayer = _pipelineContext.PlayerEntitiesDict[currentAttackerIndex];
            int currentHeroEntityIndex = _pipelineContext.AttackerHeroEntityIndex;
            
            currentPlayer.HeroEntities.TryGetHeroView(currentHeroEntityIndex, out HeroView attackerHeroView);
            _visualPipeline.AddGameTask(new ToggleHeroViewTask(attackerHeroView, true));
            
            Finish();
        }
    }
}