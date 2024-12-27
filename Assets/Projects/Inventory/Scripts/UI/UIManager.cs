using System.Collections.Generic;
using UnityEngine;

namespace Projects.Inventory.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Transform backpackSlots;
        [SerializeField] private Transform headSlot;
        [SerializeField] private Transform bodySlot;
        [SerializeField] private Transform leftHandSlot;
        [SerializeField] private Transform rightHandSlot;
        [SerializeField] private Transform feetSlot;
        [SerializeField] private GameObject backpackSlotPrefab;
        public IReadOnlyList<Transform> BackpackSlots => _backpackSlots;
        private readonly List<Transform> _backpackSlots = new();
        
        public Transform HeadSlot => headSlot;
        public Transform BodySlot => bodySlot;
        public Transform RightHandSlot => rightHandSlot;
        public Transform LeftHandSlot => leftHandSlot;
        public Transform FeetSlot => feetSlot;
        

        private void Awake()
        {
            foreach (Transform child in backpackSlots)
            {
                _backpackSlots.Add(child);
            }
        }
    }
}