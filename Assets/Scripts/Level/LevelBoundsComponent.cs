using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBoundsComponent
    {
        private Transform leftBorder;
        private Transform rightBorder;
        private Transform topBorder;
        private Transform downBorder;

        public LevelBoundsComponent(Transform leftBorder, Transform rightBorder, Transform topBorder, Transform downBorder)
        {
            this.leftBorder = leftBorder;
            this.rightBorder = rightBorder;
            this.topBorder = topBorder;
            this.downBorder = downBorder;
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > this.leftBorder.position.x
                   && positionX < this.rightBorder.position.x
                   && positionY > this.downBorder.position.y
                   && positionY < this.topBorder.position.y;
        }
    }
}