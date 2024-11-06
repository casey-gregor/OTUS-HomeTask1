using UnityEngine;

namespace Inventory
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        [field: SerializeField]
        public int Health { get; set; } = 0;
        
        [field: SerializeField]
        public int Armor {get; set;} = 0;

        [field: SerializeField]
        public int Attack { get; set; } = 0;
        
        [field: SerializeField]
        public int Speed  { get; set; } = 0;
    }
}