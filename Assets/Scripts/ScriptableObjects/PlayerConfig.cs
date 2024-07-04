using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerConfig", 
    menuName = "Configs/New PlayerConfig"
    )]
public class PlayerConfig : EntityConfig
{
    public float moveSpeed = 5f;
    public int hitPoints = 5;
}
