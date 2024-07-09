using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {
        private Transform firePoint;

        public Transform GetFirePoint(GameObject obj)
        {
            foreach (Transform child in obj.transform)
            {
                if (child.tag == IdCollection.firePointTag)
                {
                    this.firePoint = child;
                }
                else
                {
                    this.firePoint = obj.transform;
                    Debug.LogError($"No FirePoint child is found on {obj.name}. Assigned {obj.name} as the firePoint.");
                }
            }
            return this.firePoint;
        }
        public Vector2 Position
        {
            get { return this.firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return this.firePoint.rotation; }
        }

        

    }
}