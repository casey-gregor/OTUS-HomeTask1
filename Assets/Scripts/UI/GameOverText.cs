using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class GameOverText : MonoBehaviour, IGameFinishListener
    {
        [SerializeField] private GameManager gameManager;

        private TextMeshProUGUI _textMeshPro;

        //private void Start()
        //{
        //    IGameListener.Register(this);
        //}

        public void OnFinish()
        {
            _textMeshPro.gameObject.SetActive(true);
            _textMeshPro.text = "Game Over";
            _textMeshPro.fontSize = 80;
        }

        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError($"{this.name} is missing GameManager");
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (_textMeshPro == null)
                Debug.LogError($"{this.name} is missing TextMesh component");
            _textMeshPro.gameObject.SetActive( false );
        }

    }

}
