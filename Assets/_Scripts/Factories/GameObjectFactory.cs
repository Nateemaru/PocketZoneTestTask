using UnityEngine;
using Zenject;

namespace _Scripts.Factories
{
    public class GameObjectFactory : PlaceholderFactory<GameObject>
    {
        private DiContainer _container;

        public GameObjectFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject CreateGameObject(GameObject prefab, Transform parent = null)
        {
            var result = _container.InstantiatePrefab(prefab, parent);
            return result;
        }
    }
}