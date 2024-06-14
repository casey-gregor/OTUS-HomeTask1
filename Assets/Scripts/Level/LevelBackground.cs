using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private Params parameters;

        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;

        private Transform _transform;


        private void Awake()
        {
            this.startPositionY = this.parameters.startPositionY;
            this.endPositionY = this.parameters.endPositionY;
            this.movingSpeedY = this.parameters.movingSpeedY;
            this._transform = this.transform;
            var position = this._transform.position;
            this.positionX = position.x;
            this.positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (this._transform.position.y <= this.endPositionY)
            {
                this._transform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this._transform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            public float startPositionY;

            [SerializeField]
            public float endPositionY;

            [SerializeField]
            public float movingSpeedY;
        }
    }
}