using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class InventorySlotView : BaseButton
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _amountTXT;
        
        public bool IsReserved { get; private set; }

        public override void OnClick()
        {
        }

        public void SetData(Sprite sprite, string text)
        {
            _iconImage.sprite = sprite;
            _amountTXT.text = text;
            IsReserved = true;
        }
    }
}