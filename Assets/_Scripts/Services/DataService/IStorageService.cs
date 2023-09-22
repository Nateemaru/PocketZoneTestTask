using System;

namespace _Scripts.Services.DataService
{
    public interface IStorageService
    {
        public void Save<T>(string key, T data, Action callback = null);
        public T Load<T>(string key);
    }
}