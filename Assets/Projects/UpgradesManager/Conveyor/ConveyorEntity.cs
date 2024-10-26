using Entities;
using UnityEngine;

namespace UpgradesManager
{
    public class ConveyorEntity : MonoEntityBase
    {
        [SerializeField] private ConveyorModel _model;
        
        private void Awake()
        {
            Add(new Conveyor_SetLoadStorageComponent(_model.LoadStorageCapacity));            
            Add(new Conveyor_SetUnloadStorageComponent(_model.UnloadStorageCapacity));            
            Add(new Conveyor_SetProduceTimeComponent(_model.ProduceTime));            
        }
        
    }
}