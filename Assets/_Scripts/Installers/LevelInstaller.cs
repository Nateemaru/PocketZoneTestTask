using _Scripts.Factories;
using _Scripts.Services.StateMachines.LevelStateMachine;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindLevelStateMachine();
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
