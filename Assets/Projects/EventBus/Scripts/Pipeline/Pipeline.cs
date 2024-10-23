using System;
using System.Collections.Generic;

namespace EventBus
{
    public abstract class Pipeline
    {
        public Action OnPipelineCompleted;
        
        private readonly List<GameTask> _gameTasks = new();
        private int _taskIndex = -1;

        public void AddGameTask(GameTask gameTask)
        {
            _gameTasks.Add(gameTask);
        }

        public void ResetPipeline()
        {
            ClearPipeline();
            ResetTasksIndex();
        }
        public void ClearPipeline()
        {
            _gameTasks.Clear();
        }

        public void ResetTasksIndex()
        {
            _taskIndex = -1;
        }
        
        public int TasksCount => _gameTasks.Count;
        public void Run()
        {
            _taskIndex++;

            if (_taskIndex >= _gameTasks.Count)
            {
                OnPipelineCompleted?.Invoke();
                return;
            }

            _gameTasks[_taskIndex].Run(OnFinish);
        }
        
        private void OnFinish()
        {
            Run();
        }

    }
    
}
