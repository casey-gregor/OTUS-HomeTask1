using System;
using UnityEngine;
using UnityEngine.UI;

namespace RealTime
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickProcessor : MonoBehaviour
    {
        [SerializeField] private ChestView _chestView;
        public event Action<ChestView> OnButtonClick;
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleClick);

            if (_chestView == null)
            {
                Debug.LogWarning("No chest view assigned to ButtonClickProcessor.");
            }
        }

        private void HandleClick()
        {
            OnButtonClick?.Invoke(_chestView);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}