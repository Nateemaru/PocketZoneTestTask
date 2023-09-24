using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(menuName = "SO/ClothesItemConfig", fileName = "ClothesItemConfig")]
    public class ClothesItemConfig : BaseItemConfig
    {
        [SerializeField] private int _armor;

        public int Armor => _armor;
    }
}