using Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace UpgradesManager
{
    public class ConveyorEntity : MonoEntityBase
    {
        [SerializeField] private ConveyorModel model;
        
        private void Awake()
        {
            Add(new Conveyor_SetLoadStorageComponent(model.LoadStorageCapacity));            
            Add(new Conveyor_SetUnloadStorageComponent(model.UnloadStorageCapacity));            
            Add(new Conveyor_SetProduceTimeComponent(model.ProduceTime));            
        }
        
    }
}