using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace EventBus
{
    [Serializable]
    public class HeroConfig
    {
        public Sprite heroImage;
        public int attack;
        public int health;
        [SerializeReference] public IEffect effect;
    }
}