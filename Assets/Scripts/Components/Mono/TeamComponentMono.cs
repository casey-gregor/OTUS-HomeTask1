using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponentMono : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        public bool IsPlayer
        {
            get { return this.isPlayer; }
        }
        
    }
}