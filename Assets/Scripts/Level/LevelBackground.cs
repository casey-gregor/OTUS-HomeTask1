using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : IGameFixedUpdateListener
    {

        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;

        private LevelbackgroundConfig config;

        private Transform objTransform;

        public LevelBackground(LevelbackgroundConfig config, GameObject obj)
        {
            this.config = config;

            this.startPositionY = this.config.startPositionY;
            this.endPositionY = this.config.endPositionY;
            this.movingSpeedY = this.config.movingSpeedY;

            this.objTransform = obj.transform;
            var position = this.objTransform.position;
            this.positionX = position.x;
            this.positionZ = position.z;

            IGameListener.Register(this);
        }

        public void OnFixedUpdate()
        {
            if (this.objTransform.position.y <= this.endPositionY)
            {
                this.objTransform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.objTransform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * Time.fixedDeltaTime,
                this.positionZ
            );
        }
    }
}