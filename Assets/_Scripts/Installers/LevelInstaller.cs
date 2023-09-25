using _Scripts.Factories;
using _Scripts.Game;
using _Scripts.Game.AI;
using _Scripts.Game.InventorySystem;
using _Scripts.Game.PlayerCore;
using _Scripts.Services;
using _Scripts.Services.InputService;
using _Scripts.Services.StateMachines.LevelStateMachine;
using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private BaseInputService _inputService;
        
        public override void InstallBindings()
        {
            BindFactories();
            BindLevelStateMachine();
            BindInputService();
            BindInventory();
            BindInventoryView();
            BindEquipmentInventoryView();
            BindPlayer();
            BindEnemiesHasher();
            BindAmmoHandler();
            BindDropHandler();
            BindEnemyFactory();
        }

        private void BindEnemyFactory()
        {
            Container
            .BindFactory<GameObject, BaseEnemy, BaseEnemy.Factory>()
            .FromFactory<EnemyFactory>()
            .WhenInjectedInto(new []
            {
                typeof(LevelBootstrap),
            });
        }

        private void BindDropHandler()
        {
            Container
                .Bind<DropHandler>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindAmmoHandler()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerShootment>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemiesHasher()
        {
            Container
            .Bind<EnemiesHasher>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        }

        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerController>()
                .FromComponentsInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindEquipmentInventoryView()
        {
            Container
            .Bind<EquipmentInventoryView>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
        }

        private void BindInventoryView()
        {
            Container
                .Bind<InventoryView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindInventory()
        {
            Container
                .Bind<Inventory>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindInputService()
        {
            Container
                .Bind<BaseInputService>()
                .FromInstance(_inputService)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindFactories()
        {
            Container.BindFactory<GameObject, GameObjectFactory>().FromFactory<GameObjectFactory>();
        }

        private void BindLevelStateMachine()
        {
            Container
                .Bind<ILevelStateMachine>()
                .To<LevelStateMachine>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
