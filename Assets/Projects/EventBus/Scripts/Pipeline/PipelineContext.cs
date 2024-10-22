using System.Collections.Generic;
using UI;
using Zenject.SpaceFighter;

namespace EventBus
{
    public class PipelineContext
    {
        public readonly int NumberOfPlayers = 2;

        public int AttackerIndex { get; private set; }
        public int TargetIndex { get; private set; }
        public int AttackerHeroEntityIndex { get; private set; }
        
        public HeroEntity AttackerHeroEntity { get; private set; }
        public HeroEntity TargetHeroEntity { get; private set; }

        public Dictionary<int, PlayerEntity> PlayerEntitiesDict { get; private set; }


        public void SetAttackerIndex(int value)
        {
            AttackerIndex = value;
        }

        public void SetTargetIndex(int targetEntityIndex)
        {
            TargetIndex = targetEntityIndex;
        }
        
        public void SetPlayerEntitiesDict(Dictionary<int, PlayerEntity> value)
        {
            PlayerEntitiesDict = value;
        }

        public void SetAttackerHeroEntityIndex(int value)
        {
            AttackerHeroEntityIndex = value;
        }
        
        // public void SetTargetHeroEntityIndex(int value)
        // {
        //     TargetHeroEntityIndex = value;
        // }

        public void SetAttackerHeroEntity(HeroEntity value)
        {
            AttackerHeroEntity = value;
        }

        public void SetTargetHeroEntity(HeroEntity value)
        {
            TargetHeroEntity = value;
        }
        

    }
}