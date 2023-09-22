using System;
using System.IO;
using UnityEngine;

namespace _Scripts.Services.DataService
{
    public class JsonToPlayerPrefsStorage : IStorageService
    {
        public void Save<TData>(string key, TData data, Action callback = null)
        {
            if(PlayerPrefs.HasKey(key))
                PlayerPrefs.DeleteKey(key);
            
            string json = JsonUtility.ToJson(data);
            
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            
            callback?.Invoke();
        }

        public TData Load<TData>(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                TData data = JsonUtility.FromJson<TData>(json);
                return data;
            }

            return default;
        }
    }
}