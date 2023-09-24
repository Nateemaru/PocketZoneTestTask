using _Scripts.Game.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class EquipmentSlotView : BaseButton
    {
        [SerializeField] private EquipType _equipType;
        [SerializeField] private Image _iconImage;

        public EquipType EquipType => _equipType;
        
        public override void OnClick()
        {
        }

        public void SetData(Sprite sprite)
        {
            _iconImage.sprite = sprite;
        }
        
        public void ClearData()
        {
            _iconImage.sprite = null;
        }
    }
}