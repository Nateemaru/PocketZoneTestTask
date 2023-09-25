using _Scripts.Game.AI;
using UnityEngine;
using Zenject;

namespace _Scripts.Factories
{
    public class EnemyFactory : IFactory<GameObject, BaseEnemy>
    {
        private DiContainer _container;

        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }

        public BaseEnemy Create(GameObject prefab)
        {
            var enemy = _container.InstantiatePrefabForComponent<BaseEnemy>(prefab);
            return enemy;
        }
    }
}