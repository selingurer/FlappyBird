using System;
using UnityEngine;

namespace Service
{
    public class SaveLoadService : ISaveLoadService
    {
        [Serializable]
        private class Wrapper<T>
        {
            public T Value;
        }
        
        public void Save<T>(string key, T data)
        {
            string json = JsonUtility.ToJson(new Wrapper<T> { Value = data });
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<Wrapper<T>>(json).Value;
        }
    }

    public interface ISaveLoadService
    {
        void Save<T>(string key, T data);
        T Load<T>(string key, T defaultValue = default);
    }
}