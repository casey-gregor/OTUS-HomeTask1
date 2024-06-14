using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        private const string firePointTag = "FirePoint";
        private Transform _firePoint;

        private void Awake()
        {
            foreach(Transform child in transform)
            {
                if (child.tag == firePointTag)
                {
                    _firePoint = child;
                }
                else
                {
                    _firePoint = this.transform;
                    Debug.LogError($"No FirePoint child is found on {this.name}. Assigned {this.name} as the firePoint.");
                }
            }
        }

        public Vector2 Position
        {
            get { return this._firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this._firePoint.rotation; }
        }

        

    }
}