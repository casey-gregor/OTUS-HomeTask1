using UnityEngine;

namespace EventBus
{
    public sealed class LaunchVisualPipelineTask : GameTask
    {
        private readonly VisualPipeline _visualPipeline;

        public LaunchVisualPipelineTask(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void OnRun()
        {
            Debug.Log("LaunchVisualPipelineTask started ");

            if (_visualPipeline.TasksCount != 0)
            {
                _visualPipeline.OnPipelineCompleted += HandleVisualPipelineComplete;
                _visualPipeline.Run();
            }
            else
            {
                Finish();
            }
        }

        private void HandleVisualPipelineComplete()
        {
            _visualPipeline.ResetPipeline();
            Finish();
        }

        protected override void OnFinish()
        {
            _visualPipeline.OnPipelineCompleted -= HandleVisualPipelineComplete;
        }
    }
}