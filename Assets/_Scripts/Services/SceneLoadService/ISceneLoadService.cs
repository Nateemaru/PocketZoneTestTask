using System;

namespace _Scripts.Services.SceneLoadService
{
    public interface ISceneLoadService
    {
        public void Load(string name, Action onLoaded = null);
    }
}