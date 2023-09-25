using _Scripts.Game.InventorySystem;
using UnityEngine;

namespace _Scripts.Configs
{
    public class BaseItemConfig : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private EquipType _equipType;
        [SerializeField] private  string _Id;
        [SerializeField] private  Sprite _icon;
        [SerializeField] private  GameObject _prefab;

        public ItemType ItemType => _itemType;
        public string ID => _Id;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;

        public EquipType EquipType => _equipType;
    }
}