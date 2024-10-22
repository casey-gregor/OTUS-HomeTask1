using UnityEngine;

namespace UI
{
    public sealed class UIService : MonoBehaviour
    {
        [SerializeField]
        private HeroListView redPlayer;
        
        [SerializeField]
        private HeroListView bluePlayer;
        
        [SerializeField]
        private GameObject gameOverPanel;
        
        [SerializeField]
        private GameObject infoPanel;

        [SerializeField] 
        private GameObject crossImagePrefab;

        private HeroListView[] playersArray;

        private void Awake()
        {
            playersArray = new[] { redPlayer, bluePlayer };
        }

        public HeroListView GetBluePlayer()
        {
            return this.bluePlayer;
        }

        public HeroListView GetRedPlayer()
        {
            return this.redPlayer;
        }

        public HeroListView[] GetPlayers()
        {
            return playersArray;
        }

        public GameObject GetGameOverPanel()
        {
            return gameOverPanel;
        }

        public GameObject GetCrossImagePrefab()
        {
            return crossImagePrefab;
        }

        public GameObject GetInfoPanel()
        {
            return infoPanel;
        }
        
    }
}