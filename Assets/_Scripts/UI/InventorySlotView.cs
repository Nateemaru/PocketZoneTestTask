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
        
        public string ItemID { get; private set; }
        
        public bool IsReserved { get; private set; }

        public event Action<string, InventorySlotView> OnClicked;

        public override void OnClick()
        {
            OnClicked?.Invoke(ItemID, this);
        }

        public void SetData(string itemID, Sprite sprite, string text)
        {
            ItemID = itemID;
            _iconImage.sprite = sprite;
            _amountTXT.text = text;
            IsReserved = true;
        }

        public void ClearData()
        {
            OnClicked = null;
            _iconImage.sprite = null;
            _amountTXT.text = "";
            IsReserved = false;
        }
    }
}