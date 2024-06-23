using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameOverText : MonoBehaviour, IGameFinishListener
    {
        [Inject] private GameManager gameManager;

        private TextMeshProUGUI textMeshPro;

        public void OnFinish()
        {
            textMeshPro.gameObject.SetActive(true);
            textMeshPro.text = "Game Over";
            textMeshPro.fontSize = 80;
        }

        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError($"{this.name} is missing GameManager");
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro == null)
                Debug.LogError($"{this.name} is missing TextMesh component");
            textMeshPro.gameObject.SetActive( false );
        }

    }

}
