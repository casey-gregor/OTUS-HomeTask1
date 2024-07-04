using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyConfig", 
    menuName = "Configs/New EnemyConfig"
    )]
public class EnemyConfig : ScriptableObject
{
    public float timeBetweenShots = 3;
    public int hitPoints = 3;

}
