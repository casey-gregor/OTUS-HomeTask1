using System;

namespace EventBus
{
    public abstract class GameTask
    {
        private Action _onComplete;
        public void Run(Action onComplete)
        {
            _onComplete = onComplete;
            OnRun();
        }
        
        public void Finish()
        {
            OnFinish();
            _onComplete?.Invoke();
        }

        protected virtual void OnRun()
        {
        }

        protected virtual void OnFinish()
        {
        }
    }
}