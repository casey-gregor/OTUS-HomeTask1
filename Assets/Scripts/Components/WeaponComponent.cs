using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {
        private const string firePointTag = "FirePoint";
        private Transform _firePoint;


        public WeaponComponent()
        {
            
        }
        public Transform GetFirePoint(GameObject obj)
        {
            foreach (Transform child in obj.transform)
            {
                if (child.tag == firePointTag)
                {
                    _firePoint = child;
                }
                else
                {
                    _firePoint = obj.transform;
                    Debug.LogError($"No FirePoint child is found on {obj.name}. Assigned {obj.name} as the firePoint.");
                }
            }
            return _firePoint;
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