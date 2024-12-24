using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class TypeToParentConverter
    {
        public Transform BackpackParent;
        public Transform HeadParent;
        public Transform BodyParent;
        public Transform LeftHandParent;
        public Transform RightHandParent;
        public Transform LeftFootParent;
        public Transform RightFootParent;

        public Transform GetBackpackParent(SlotType slotType)
        {
            switch (slotType)
            {
                case SlotType.Head:
                    return HeadParent;
                case SlotType.Body:
                    return BodyParent;
                case SlotType.Arms
            }
        }
    }
}