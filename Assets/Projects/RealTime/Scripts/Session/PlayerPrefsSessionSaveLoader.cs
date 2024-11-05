using UnityEngine;

namespace RealTime
{
    public sealed class PlayerPrefsSessionSaveLoader : ISessionSaveLoader
    {
        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}