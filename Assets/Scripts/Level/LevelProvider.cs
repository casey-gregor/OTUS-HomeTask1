using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelProvider
    {
        public Transform worldTransform { get; private set; }
        public Transform player { get; private set; }
        public Transform enemyContainer { get; private set; }
        public GameObject background { get; private set; }


        public LevelProvider
            (
            Transform worldTransform, 
            Transform character, 
            Transform enemyContainer, 
            GameObject background
            )
        {
            this.worldTransform = worldTransform;
            this.player = character;
            this.enemyContainer = enemyContainer;
            this.background = background;

        }
    }

}
