using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(menuName = "SO/ItemsContainerConfig", fileName = "ItemsContainerConfig")]
    public class ItemsContainerConfig : ScriptableObject
    {
        [SerializeField] private BaseItemConfig[] _itemsConfigs;

        public BaseItemConfig[] ItemsConfigs => _itemsConfigs;
    }
}