using TMPro;
using UnityEngine;

namespace EventBus
{
    public sealed class GameOverTask: GameTask
    {
        private readonly GameObject _gameOverPanel;
        private readonly PlayerEntity _playerEntity;

        public GameOverTask(GameObject gameOverPanel, PlayerEntity playerEntity)
        {
            _gameOverPanel = gameOverPanel;
            _playerEntity = playerEntity;
        }

        protected override void OnRun()
        {
            GameObject gameOverPanel = _gameOverPanel;
            TextMeshProUGUI text = gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();

            text.text = GetWinText(_playerEntity);
            gameOverPanel.SetActive(true);
        }

        private string GetWinText(PlayerEntity player)
        {
            return $"{player.PlayerView.name} won! Game over.";
        }
    }
}