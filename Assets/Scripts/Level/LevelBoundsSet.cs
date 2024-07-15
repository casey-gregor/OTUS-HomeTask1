using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBoundsSet
    {
        public Transform left { get; private set; }
        public Transform right { get; private set; }
        public Transform top { get; private set; }
        public Transform bottom { get; private set; }

        public LevelBoundsSet(Transform left, Transform right, Transform top, Transform bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }
    }

}

