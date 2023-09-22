using System;
using System.Collections;
using _Scripts.Services.CoroutineRunnerService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Services.SceneLoadService
{
    public class SceneLoader : ISceneLoadService
    {
        private readonly ICoroutineRunner coroutineRunner;
        private bool _isSceneLoading = false;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            this.coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null)
        {
            if (!_isSceneLoading)
            {
                coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
            }
        }
    
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            _isSceneLoading = true;
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }
      
            onLoaded?.Invoke();
            _isSceneLoading = false;
        }
    }
}