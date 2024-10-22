using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Zenject;

namespace EventBus
{
    public class PipelineRunner : MonoBehaviour
    {
        public AllHeroesConfig config;
        private LogicPipeline _logicPipeline;
        private PlayerEntitiesInitiator _initiator;
        private VisualPipeline _visualPipeline;
        private EventBus _eventBus;
        private PipelineContext _pipelineContext;

        private const int StartingPlayerIndex = 0;
        private const int StartingHeroIndex = 0;

        [Inject]
        private void Construct(LogicPipeline logicPipeline, PlayerEntitiesInitiator initiator,
            VisualPipeline visualPipeline, EventBus eventBus, PipelineContext pipelineContext)
        {
            _logicPipeline = logicPipeline;
            _initiator = initiator;
            _visualPipeline = visualPipeline;
            _eventBus = eventBus;
            _pipelineContext = pipelineContext;

            _logicPipeline.OnPipelineCompleted += HandleLogicPipelineCompleteEvent;
        }

        private void HandleLogicPipelineCompleteEvent()
        {
            _logicPipeline.ResetTasksIndex();
            _logicPipeline.Run();
        }

        private void Start()
        {
            if (config == null || config.heroes.Length == 0)
            {
                Debug.LogWarning("No hero configs found!");
                return;
            }
            _initiator.CreatePlayerEntities(config); 
            RunPipeline();
        }
        
        private void RunPipeline()
        {
            Dictionary<int, PlayerEntity> playerEntities = _initiator.GetPlayerEntitiesDict();
            PlayerEntity attackerEntity = playerEntities[StartingPlayerIndex];
            HeroView attackHeroView = attackerEntity.HeroViews[StartingHeroIndex];
            
            ToggleHeroViewTask toggleHeroViewTask = new ToggleHeroViewTask(attackHeroView, true);
            
            _pipelineContext.SetPlayerEntitiesDict(playerEntities);
            _pipelineContext.SetAttackerIndex(StartingPlayerIndex);
            _pipelineContext.SetAttackerHeroEntityIndex(StartingHeroIndex);
            
            _visualPipeline.AddGameTask(toggleHeroViewTask);
            _logicPipeline.Run();
        }

        private void OnDestroy()
        {
            _logicPipeline.OnPipelineCompleted -= HandleLogicPipelineCompleteEvent;
        }
    }
}