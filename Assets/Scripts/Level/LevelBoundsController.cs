using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBoundsController
    {
        private Transform leftBorder;
        private Transform rightBorder;
        private Transform topBorder;
        private Transform bottomBorder;

        private LevelBoundsSet levelBoundsSet;

        public LevelBoundsController(LevelBoundsSet levelBoundsSet)
        {
            this.levelBoundsSet = levelBoundsSet;
            this.leftBorder = this.levelBoundsSet.left;
            this.rightBorder = this.levelBoundsSet.right;
            this.topBorder = this.levelBoundsSet.top;
            this.bottomBorder = this.levelBoundsSet.bottom;
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > this.leftBorder.position.x
                   && positionX < this.rightBorder.position.x
                   && positionY > this.bottomBorder.position.y
                   && positionY < this.topBorder.position.y;
        }
    }
}