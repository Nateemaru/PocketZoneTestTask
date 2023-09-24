using _Scripts.Factories;
using _Scripts.Game.InventorySystem;
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
