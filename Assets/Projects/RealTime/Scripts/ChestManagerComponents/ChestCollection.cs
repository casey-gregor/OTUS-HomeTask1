using System;
using System.Collections.Generic;

namespace RealTime
{
    [Serializable]
    public sealed class ChestCollection
    {
        public List<ChestSaveData> chests = new();
    }
}