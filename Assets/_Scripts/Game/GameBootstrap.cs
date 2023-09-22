using System;
using _Scripts.Services;
using _Scripts.Services.DataService;
using _Scripts.Services.SceneLoadService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private IDataReader _dataReader;
        private ISceneLoadService _sceneLoadService;

        [Inject]
        private void Construct(IDataReader dataReader, ISceneLoadService sceneLoadService)
        {
            _dataReader = dataReader;
            _sceneLoadService = sceneLoadService;
        }

        private void Start()
        {
            _dataReader.Init();
            _sceneLoadService.Load(GlobalConstants.GameScene);
        }
    }
}