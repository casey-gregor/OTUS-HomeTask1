using UnityEngine;

namespace EventBus
{
    public struct GameOverEvent
    {
        public PlayerEntity Winner;

        public GameOverEvent(PlayerEntity winner)
        {
            Winner = winner;
        }
    }
}