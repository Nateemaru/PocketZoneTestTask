using _Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.AI
{
    public class DropHandler
    {
        private ItemsContainerConfig _itemsContainerConfig;

        [Inject]
        private void Construct(ItemsContainerConfig itemsContainerConfig)
        {
            _itemsContainerConfig = itemsContainerConfig;
        }
        
        public void Drop(Vector3 dropPosition)
        {
            if (_itemsContainerConfig != null)
            {
                BaseItemConfig config =
                    _itemsContainerConfig.ItemsConfigs[Random.Range(0, _itemsContainerConfig.ItemsConfigs.Length)];
                GameObject model = Object.Instantiate(config.Prefab, dropPosition, Quaternion.identity);
            }
        }
    }
}