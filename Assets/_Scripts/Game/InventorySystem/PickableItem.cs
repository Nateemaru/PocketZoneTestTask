using _Scripts.Configs;
using UnityEngine;

namespace _Scripts.Game.InventorySystem
{
    public class PickableItem : MonoBehaviour
    {
        [SerializeField] private BaseItemConfig _itemConfig;

        public BaseItemConfig ItemConfig => _itemConfig;
    }
}