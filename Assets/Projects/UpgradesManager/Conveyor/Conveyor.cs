using UnityEngine;

namespace UpgradesManager
{
    public sealed class Conveyor : MonoBehaviour
    {
        public ConveyorEntity conveyorEntity;
        public ConveyorModel conveyorModel;

        private void Awake()
        {
            if (conveyorEntity == null)
            {
                Debug.LogError("ConveyorEntity is not specified in the Inspector");
            }

            if (conveyorModel == null)
            {
                Debug.LogError("ConveyorModel is not specified in the Inspector");
            }
        }
    }
}