using UnityEngine;

namespace EventBus
{
    public sealed class IdentifyAttackerHeroIndexTask : GameTask
    {
        private readonly PipelineContext _context;
        
        private int _turnNum = 0;
        
        public IdentifyAttackerHeroIndexTask(PipelineContext context)
        {
            _context = context;
        }

        protected override void OnRun()
        {
            Debug.Log("IdentifyAttackerHeroIndexTask started");
            _turnNum++;
            
            int attackerHeroEntityIndex = SetCurrentAttackHeroIndex();
            _context.SetAttackerHeroEntityIndex(attackerHeroEntityIndex);

            Finish();
        }
        
        private int SetCurrentAttackHeroIndex()
        {
            
            PlayerEntity player = _context.PlayerEntitiesDict[_context.AttackerIndex];
            
            if (_turnNum == 1)
            {
                return _context.AttackerHeroEntityIndex;
            }
            int heroCount = player.HeroComponent.GetAllHeroesCount();
            int lastAttackerHeroIndex = player.HeroComponent.LastAttackHeroIndex;
            
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
                if(playerEntity.HeroComponent.HeroEntities.TryGetHeroEntity
                       (index, out HeroEntity heroEntity) && !heroEntity.HealthComponent.IsDead)
                {
                    if (heroEntity.TurnComponent.SkipNumOfTurns == 0)
                    {
                        playerEntity.HeroComponent.LastAttackHeroIndex = index;
                        return index;
                    }
                    heroEntity.TurnComponent.EditSkipTurns(-1);
                    if (playerEntity.HeroComponent.GetAliveHeroesCount() == 1)
                        return -1;
                }
                index = (index + 1) % heroCount;

            } while (index != startingIndex);
            
            return -1;
        }
        
    }
}