using Atomic.Extensions;
using UnityEngine;

namespace ZombieShooter
{
    public class GameoverTextManager : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private GameObject popupObject;

        private void Awake()
        {
            var isDeadObservable = character.GetObservable<bool>(CharacterAPIKeys.IS_DEAD);
            isDeadObservable.Subscribe(value =>
            {
                isDeadObservable.Unsubscribe(HandleCharacterDead);
                if (value)
                    HandleCharacterDead(value);
            });
        }

        private void HandleCharacterDead(bool value)
        {
            popupObject.SetActive(value);
        }
    }
}