using UnityEngine;

namespace ZombieShooter
{
    public sealed class WinTextManager : MonoBehaviour
    {
        [SerializeField] private ZombieSpawnController zombieSpawner;
        [SerializeField] private GameObject popupObject;
        
        void Awake()
        {
            zombieSpawner.ZombiesAlive.Subscribe(value =>
            {
                if(value <= 0)
                    HandleZeroZombies();
            });
        }


        private void HandleZeroZombies()
        {
            popupObject.SetActive(true);
        }
    }
}