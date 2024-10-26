using System;
using Atomic.Elements;
using Declarative;

[Serializable]
public class ConveyorModel : DeclarativeModel
{
    public AtomicVariable<int> LoadStorageCapacity;
    public AtomicVariable<int> UnloadStorageCapacity;
    public AtomicVariable<float> ProduceTime;
    
}
    
