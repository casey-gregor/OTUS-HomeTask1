using System;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickProcessor : MonoBehaviour
    {
        public event Action<Chest> OnButtonClick;
        private Button _button;
        private Chest _chest;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _chest = GetComponentInParent<Chest>();
            
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            OnButtonClick?.Invoke(_chest);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}