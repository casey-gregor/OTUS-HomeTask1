using System.Collections.Generic;

namespace EventBus
{
    public sealed class PipelineContext
    {
        public readonly int NumberOfPlayers = 2;

        public int AttackerIndex { get; private set; }
        public int TargetIndex { get; private set; }
        public int AttackerHeroEntityIndex { get; private set; }
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

    }
}