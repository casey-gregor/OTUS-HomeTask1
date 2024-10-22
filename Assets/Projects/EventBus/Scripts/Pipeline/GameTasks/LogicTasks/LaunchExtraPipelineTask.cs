using UnityEngine;

namespace EventBus
{
    public sealed class LaunchExtraPipelineTask : GameTask
    {
        private readonly ExtraPipeline _extraPipeline;

        public LaunchExtraPipelineTask(ExtraPipeline extraPipeline)
        {
            _extraPipeline = extraPipeline;
        }
        
        protected override void OnRun()
        {
            Debug.Log("Launch ExtraPipelineTask");

            if (_extraPipeline.TasksCount != 0)
            {
                _extraPipeline.OnPipelineCompleted += HandleExtraPipelineComplete;
                _extraPipeline.Run();
            }
            else
            {
                Finish();
            }
        }

        private void HandleExtraPipelineComplete()
        {
            _extraPipeline.ResetPipeline();
            Finish();
        }

        protected override void OnFinish()
        {
            _extraPipeline.OnPipelineCompleted -= HandleExtraPipelineComplete;
        }
    }
}