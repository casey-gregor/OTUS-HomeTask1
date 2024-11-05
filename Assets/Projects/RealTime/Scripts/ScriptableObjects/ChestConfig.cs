using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RealTime
{
    [CreateAssetMenu(fileName = "ChestConfig", menuName = "RealTime/New ChestConfig", order = 0)]
    public class ChestConfig : ScriptableObject
    {
        public string chestId;
        public ChestType chestType;
        public GameObject chestPrefab;
        [FormerlySerializedAs("minutesToOpen")] public int minutesBeforeOpen;
        [SerializeReference] public List<IReward> bonuses;
    }
}