using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterMoveAgent : MoveComponent, IFixedTickable
    {
        private PlayerConfig playerConfig;
        private InputManager inputManager;
        private Rigidbody2D rb;

        public CharacterMoveAgent(PlayerConfig playerConfig, InputManager inputManager, Rigidbody2D rb)
        {
            this.playerConfig = playerConfig;
            this.inputManager = inputManager;
            this.rb = rb;
        }
     
        public void FixedTick()
        {
            Move(rb, new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime, playerConfig.moveSpeed);        
        }
    }
}
