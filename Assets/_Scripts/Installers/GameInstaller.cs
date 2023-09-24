using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using _Scripts.Services;
using _Scripts.Services.AudioService;
using _Scripts.Services.CoroutineRunnerService;
using _Scripts.Services.DataService;
using _Scripts.Services.PauseService;
using _Scripts.Services.SceneLoadService;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ItemsContainerConfig _itemsContainerConfig;
        
        public override void InstallBindings()
        {
            BindStorage();
            BindDataReader();
            BindSceneLoadService();
            BindPauseHandler();
            BindCoroutineRunner();
            BindFPSUnlocker();
            BindItemsContainerConfig();
        }

        private void BindItemsContainerConfig()
        {
            Container
                .Bind<ItemsContainerConfig>()
                .FromScriptableObject(_itemsContainerConfig)
                .AsSingle()
                .NonLazy();
        }

        private void BindFPSUnlocker()
        {
            Container
                .Bind<FPSUnlocker>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }

        private void BindDataReader()
        {
            Container
                .Bind<IDataReader>()
                .To<DataReader>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindStorage()
        {
            Container
                .Bind<IStorageService>()
                .To<JsonToFileStorage>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseHandler()
        {
            Container
                .Bind<PauseHandler>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .Bind<ISceneLoadService>()
                .To<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}