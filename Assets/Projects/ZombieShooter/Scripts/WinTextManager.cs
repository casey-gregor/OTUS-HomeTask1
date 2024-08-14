using ShootEmUp;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public class WinTextManager : MonoBehaviour
    {
        [SerializeField] ZombieSpawnController _zombieSpawner;
        private TextMeshProUGUI _winText;
        void Awake()
        {
            _winText = GetComponentInChildren<TextMeshProUGUI>();

            _zombieSpawner._zombiesAlive.Subscribe(value =>
            {
                if(value <= 0)
                    HandleZeroZombies();
            });
        }


        private void HandleZeroZombies()
        {
            _winText.enabled = true;
        }
    }
}