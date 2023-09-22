using System;
using System.IO;
using UnityEngine;

namespace _Scripts.Services.DataService
{
    public class JsonToFileStorage : IStorageService
    {
        public void Save<TData>(string key, TData data, Action callback = null)
        {
            string path = BuildPath(key); 
            
            if(File.Exists(path))
                File.Delete(path);
            
            string json = JsonUtility.ToJson(data);
            
            try {
                File.WriteAllText(path, json);
            } catch (Exception e) {
                Debug.LogError(e);
                return;
            }
            
            callback?.Invoke();
        }

        public TData Load<TData>(string key)
        {
            string path = BuildPath(key);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                TData data = JsonUtility.FromJson<TData>(json);

                return data;
            }

            return default;
        }

        private string BuildPath(string key) => Path.Combine(Application.persistentDataPath, key + ".json");
    }
}