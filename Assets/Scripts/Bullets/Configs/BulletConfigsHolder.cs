using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
           fileName = "BulletConfigsHolder",
           menuName = "Bullets/New BulletsConfigHolder"
       )]
    public class BulletConfigsHolder : ScriptableObject
    {
        public BulletConfig enemyBulletConfig;
        public BulletConfig playerBulletConfig;
    }

}
