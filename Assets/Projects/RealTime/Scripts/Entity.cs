using UnityEngine;

namespace RealTime
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        public int money;
        public int resource;
        public int Money
        {
            get => money;
            set => money = value; 
        }
        public int Resource 
        {
            get => resource;
            set => resource = value; 
        }
    }
}