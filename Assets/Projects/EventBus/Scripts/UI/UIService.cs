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

        public bool TryGetGameOverPanel(out GameObject panel)
        {
            if (gameOverPanel != null)
            {
                panel = gameOverPanel;
                return true;
            }
            panel = null;
            return false;
        }

        public bool TryGetCrossImagePrefab(out GameObject prefab)
        {
            if (crossImagePrefab != null)
            {
                prefab = crossImagePrefab;
                return true;
            }
            prefab = null;
            return false;
        }

        public bool TryGetInfoPanel(out GameObject panel)
        {
            if (infoPanel != null)
            {
                panel = infoPanel;
                return true;
            }
            panel = null;
            return false;
        }
        
    }
}