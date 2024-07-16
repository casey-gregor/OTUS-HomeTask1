using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerMoveController : IGameFixedUpdateListener
    {
        private PlayerConfig playerConfig;
        private InputManager inputManager;
        private Rigidbody2D rb;

        public PlayerMoveController
            (
            PlayerConfig playerConfig, 
            InputManager inputManager, 
            Rigidbody2D rb
            )
        {
            this.playerConfig = playerConfig;
            this.inputManager = inputManager;
            this.rb = rb;
        }
        private void Move(Rigidbody2D rb, Vector2 vector, float speed)
        {
            Vector2 nextPosition = rb.position + vector * speed;
            rb.MovePosition(nextPosition);
        }
     
        public void OnFixedUpdate()
        {
            Move(rb, new Vector2(this.inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime, this.playerConfig.moveSpeed);        
        }

    }
}
