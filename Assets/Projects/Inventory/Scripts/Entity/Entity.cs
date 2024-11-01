using UnityEngine;

namespace Inventory
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        public int health = 0;
        public int armor = 0;
        public int attack = 0;
        public int speed = 0;
        public int Health
        {
            get => health;
            set => health = value;
        }

        public int Armor
        {
            get => armor;
            set => armor = value;
        }

        public int Attack
        {
            get => attack;
            set => attack = value;
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }
    }
}