using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerHealthObserverComponent
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlayerHitPointsComponent hitPointsComponent;

        public PlayerHealthObserverComponent(GameManager gameManager, PlayerHitPointsComponent hitPointsComponent)
        {
            this.gameManager = gameManager;
            this.hitPointsComponent = hitPointsComponent;

            hitPointsComponent.hpEmptyEvent += this.HandleHPEmptyEvent;
        }

        private void HandleHPEmptyEvent(GameObject _) => this.gameManager.FinishGame();
    }

}
