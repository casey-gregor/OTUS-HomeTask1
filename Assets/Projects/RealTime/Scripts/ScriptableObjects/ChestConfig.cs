using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealTime
{
    [CreateAssetMenu(fileName = "ChestConfig", menuName = "RealTime/New ChestConfig", order = 0)]
    public class ChestConfig : ScriptableObject
    {
        [SerializeField] private string chestId;
        [SerializeField] private ChestType chestType;
        [SerializeField] private GameObject chestPrefab;
        [SerializeField] private int minutesBeforeOpen;
        [SerializeReference] private List<IReward> bonuses;

        public ChestConfigData GetChestData()
        {
            return new ChestConfigData(
                chestId,
                chestType,
                chestPrefab,
                minutesBeforeOpen,
                bonuses);
        }
    }
    
    public struct ChestConfigData : IEquatable<ChestConfigData>
    {
        public string ChestId;
        public ChestType ChestType;
        public GameObject ChestPrefab;
        public int InitialTimer;
        public List<IReward> Rewards;

        public ChestConfigData(
            string chestId, 
            ChestType chestType, 
            GameObject chestPrefab, 
            int initialTimer, 
            List<IReward> rewards)
        {
            ChestId = chestId;
            ChestType = chestType;
            ChestPrefab = chestPrefab;
            InitialTimer = initialTimer;
            Rewards = rewards;
        }
        
        public bool Equals(ChestConfigData other)
        {
            return ChestId == other.ChestId;
        }
    }
}