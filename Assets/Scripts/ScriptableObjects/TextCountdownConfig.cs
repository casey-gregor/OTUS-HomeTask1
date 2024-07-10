using UnityEngine;


namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "TextCountdownConfig", 
        menuName = "Configs/New TextCountdownConfig"
        )]
    public class TextCountdownConfig : ScriptableObject
    {
        public float timer = 3;
        public int interval = 1;
        public GameManager.State stateToSet = GameManager.State.Start;
        public bool isTextAnimatable;
    }
}
