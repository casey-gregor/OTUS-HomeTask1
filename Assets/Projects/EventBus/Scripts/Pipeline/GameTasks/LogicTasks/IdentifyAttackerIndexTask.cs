using UnityEngine;

namespace EventBus
{
    public sealed class IdentifyAttackerIndexTask : GameTask
    {
        private readonly PipelineContext _context;
        
        private int _turnNum = 0;

        public IdentifyAttackerIndexTask(PipelineContext context)
        {
            _context = context;
        }
        protected override void OnRun()
        {
            Debug.Log("IdentifyAttackerIndexTask started");
            _turnNum++;

            int currentAttackerIndex = _context.AttackerIndex;
            int numOfPlayers = _context.NumberOfPlayers;
            
            int attackerEntityIndex = _turnNum == 1
                ? currentAttackerIndex
                : (currentAttackerIndex + 1) % numOfPlayers;
            
            _context.SetAttackerIndex(attackerEntityIndex);
            
            Finish();
        }
        
        
    }
}