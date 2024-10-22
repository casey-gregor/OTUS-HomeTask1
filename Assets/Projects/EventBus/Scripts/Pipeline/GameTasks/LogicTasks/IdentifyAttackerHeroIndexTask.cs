using UnityEngine;

namespace EventBus
{
    public sealed class IdentifyAttackerHeroIndexTask : GameTask
    {
        private readonly PipelineContext _pipelineContext;
        
        private int _turnNum = 0;
        
        public IdentifyAttackerHeroIndexTask(PipelineContext pipelineContext)
        {
            _pipelineContext = pipelineContext;
        }

        protected override void OnRun()
        {
            Debug.Log("second task started");
            _turnNum++;

            int currentAttackerIndex = _pipelineContext.AttackerIndex;
            
            int attackerHeroIndex = SetCurrentAttackHeroIndex();
            
            if (attackerHeroIndex < 0)
            {
                Finish();
                return;
            }
            
            _pipelineContext.SetAttackerHeroEntityIndex(attackerHeroIndex);

            Finish();
        }
        
        private int SetCurrentAttackHeroIndex()
        {
            
            PlayerEntity player = _pipelineContext.PlayerEntitiesDict[_pipelineContext.AttackerIndex];
            
            if (_turnNum == 1)
            {
                player.HeroEntities.TryGetHeroEntity(_pipelineContext.AttackerHeroEntityIndex, out HeroEntity result);
                _pipelineContext.SetAttackerHeroEntity(result);
                return _pipelineContext.AttackerHeroEntityIndex;
            }
            int heroCount = player.GetAllHeroesCount();
            int lastAttackerHeroIndex = player.LastAttackHeroIndex;
            
            if (_turnNum > 2)
            {
                lastAttackerHeroIndex = (lastAttackerHeroIndex + 1) % heroCount;
            }
            return FindNextAliveHeroIndex(player, heroCount, lastAttackerHeroIndex);

        }

        private int FindNextAliveHeroIndex(PlayerEntity playerEntity, int heroCount, int startingIndex)
        {
            int index = startingIndex;
            do
            {
                if(playerEntity.HeroEntities.TryGetHeroEntity(index, out HeroEntity result) && !result.IsDead)
                {
                    if (result.SkipNumOfTurns == 0)
                    {
                        playerEntity.LastAttackHeroIndex = index;
                        _pipelineContext.SetAttackerHeroEntity(result);
                        // Debug.Log("attacker hero entity : " + result.View.name);
                        return index;
                    }
                    result.ModifyTurnsToSkip(-1);
                }
                index = (index + 1) % heroCount;

            } while (index != startingIndex);
            
            return -1;
        }
        
    }
}