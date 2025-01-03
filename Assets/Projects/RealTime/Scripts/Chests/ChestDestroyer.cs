namespace RealTime
{
    public class ChestDestroyer
    {
        private ChestSpawner _chestSpawner;

        public ChestDestroyer(ChestSpawner chestSpawner)
        {
            _chestSpawner = chestSpawner;
        }

        public void DestroyChest(ChestConfigData config)
        {
            for (var index = 0; index < _chestSpawner.SpawnedChests.Count; index++)
            {
                var chest = _chestSpawner.SpawnedChests[index];
                if (chest.ChestId == config.ChestId)
                {
                    _chestSpawner.SpawnedChests.Remove(chest);
                    chest.Dispose();
                }
            }
        }
    }
}