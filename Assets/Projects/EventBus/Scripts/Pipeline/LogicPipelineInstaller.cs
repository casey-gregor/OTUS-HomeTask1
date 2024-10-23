using Zenject;

namespace EventBus
{
    public sealed class LogicPipelineInstaller : IInitializable
    {
        private readonly LogicPipeline _logicPipeline;
        private readonly DiContainer _container;

        public LogicPipelineInstaller(
            LogicPipeline logicPipeline, 
            DiContainer container)
        {
            _logicPipeline = logicPipeline;
            _container = container;
        }

        public void Initialize()
        {
            _logicPipeline.AddGameTask(_container.Instantiate<IdentifyAttackerIndexTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<IdentifyAttackerHeroIndexTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<SetSwitchOnVisualTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<LaunchVisualPipelineTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<PlayerInputTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<LaunchExtraPipelineTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<LaunchVisualPipelineTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<ProcessTurnDataTask>());
            _logicPipeline.AddGameTask(_container.Instantiate<EndTurnTask>());
        }
    }
}