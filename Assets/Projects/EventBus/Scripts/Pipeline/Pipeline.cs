using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus
{
    public abstract class Pipeline
    {
        public Action OnPipelineCompleted;
        public Action OnPipelineStopped;
        
        private readonly List<GameTask> _gameTasks = new();
        private int _taskIndex = -1;
        
        private bool _stopped = false;

        public void AddGameTask(GameTask gameTask)
        {
            _gameTasks.Add(gameTask);
            // Debug.Log("added gametask : " + gameTask);
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
        
        public void Abort()
        {
            _stopped = true;
        }
        
        public int TasksCount => _gameTasks.Count;
        public void Run()
        {
            if (_stopped)
            {
                OnPipelineStopped?.Invoke();
                return;
            }
            
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
