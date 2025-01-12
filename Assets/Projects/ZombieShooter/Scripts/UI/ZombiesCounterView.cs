using System;
using TMPro;
using UnityEngine;

namespace ZombieShooter
{
    public class ZombiesCounterView : MonoBehaviour
    {
        [SerializeField] ZombieSpawnController zombieSpawner;
        [SerializeField] TextMeshProUGUI zombieCountText;
        
        private int _zombieCount;

        private void Awake()
        {
            zombieSpawner.ZombiesAlive.Subscribe(UpdateZombieCount);
        }

        private void UpdateZombieCount(int value)
        {
            zombieCountText.text = $" : {value.ToString()}";
        }
    }
}