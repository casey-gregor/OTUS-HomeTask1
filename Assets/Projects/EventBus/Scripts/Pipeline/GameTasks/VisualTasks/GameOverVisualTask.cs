using TMPro;
using UnityEngine;

namespace EventBus
{
    public sealed class GameOverVisualTask: GameTask
    {
        private readonly GameObject _gameOverPanel;
        private readonly PlayerEntity _playerEntity;

        public GameOverVisualTask(GameObject gameOverPanel, PlayerEntity playerEntity)
        {
            _gameOverPanel = gameOverPanel;
            _playerEntity = playerEntity;
        }

        protected override void OnRun()
        {
            Debug.Log("Game Over visual task");
            GameObject gameOverPanel = _gameOverPanel;
            TextMeshProUGUI text = gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();

            text.text = GetWinText(_playerEntity);
            gameOverPanel.SetActive(true);
            Debug.Log("The pipeline execution stops here.");
        }

        private string GetWinText(PlayerEntity player)
        {
            return $"{player.HeroComponent.PlayerView.name} won! Game over.";
        }
    }
}