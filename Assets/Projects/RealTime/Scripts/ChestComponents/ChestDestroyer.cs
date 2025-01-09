namespace RealTime
{
    public sealed class ChestDestroyer
    {
        private readonly ChestSpawner _chestSpawner;

        public ChestDestroyer(ChestSpawner chestSpawner)
        {
            _chestSpawner = chestSpawner;
        }

        public void DestroyChest(ChestConfigData config)
        {
            for (var index = _chestSpawner.SpawnedChests.Count-1; index >= 0; index--)
            {
                ChestPresenter chestPresenter = _chestSpawner.SpawnedChests[index];
                if (chestPresenter.ChestId == config.ChestId)
                {
                    _chestSpawner.RemoveChest(chestPresenter);
                    chestPresenter.Dispose();
                }
            }
        }
    }
}