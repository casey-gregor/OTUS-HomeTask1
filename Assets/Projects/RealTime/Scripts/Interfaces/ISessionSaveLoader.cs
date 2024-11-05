namespace RealTime
{
    public interface ISessionSaveLoader
    {
        public void SaveString(string key, string value);
        public string LoadString(string key);
        public bool HasKey(string key);
    }
}