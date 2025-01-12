using Atomic.Elements;
using Atomic.Extensions;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public sealed class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private TextMeshProUGUI hitPointsText;

        private void Awake()
        {
            character.GetObservable<int>(CharacterAPIKeys.HITPOINTS).Subscribe(UpdateHitPoints);
            int hitPoints = character.GetVariable<int>(CharacterAPIKeys.HITPOINTS).Value;
            UpdateHitPoints(hitPoints);
        }

        private void UpdateHitPoints(int value)
        {
            hitPointsText.text = $" : {value.ToString()}";
        }
    }
}