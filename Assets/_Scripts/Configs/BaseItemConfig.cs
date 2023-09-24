using UnityEngine;

namespace _Scripts.Configs
{
    public class BaseItemConfig : ScriptableObject
    {
        [SerializeField] private  string _Id;
        [SerializeField] private  Sprite _icon;
        [SerializeField] private  GameObject _prefab;

        public string ID => _Id;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
    }
}