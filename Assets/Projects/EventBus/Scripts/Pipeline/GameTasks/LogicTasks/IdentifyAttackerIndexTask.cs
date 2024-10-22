using UnityEngine;

namespace EventBus
{
    public sealed class IdentifyAttackerIndexTask : GameTask
    {
        private readonly PipelineContext _pipelineContext;
        
        private int _turnNum = 0;

        public IdentifyAttackerIndexTask(PipelineContext pipelineContext)
        {
            _pipelineContext = pipelineContext;
        }
        protected override void OnRun()
        {
            Debug.Log("first task started");
            _turnNum++;

            int currentAttackerIndex = _pipelineContext.AttackerIndex;
            int numOfPlayers = _pipelineContext.NumberOfPlayers;
            
            int attackerEntityIndex = _turnNum == 1
                ? currentAttackerIndex
                : (currentAttackerIndex + 1) % numOfPlayers;
            
            _pipelineContext.SetAttackerIndex(attackerEntityIndex);
            
            Finish();
        }
        
        
    }
}