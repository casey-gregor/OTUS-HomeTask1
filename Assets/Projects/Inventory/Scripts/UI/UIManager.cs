using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projects.Inventory.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform backpack;

        public IReadOnlyList<Transform> BackpackSlots => _backpackSlots;
        private List<Transform> _backpackSlots = new();

        private void Awake()
        {
            foreach (var bagpackSlot in backpack.GetComponentsInChildren<Transform>())
            {
                _backpackSlots.Add(bagpackSlot);
            }
        }
    }
}