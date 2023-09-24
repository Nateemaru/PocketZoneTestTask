using System;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class InventoryContextMenu : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private TMP_Text _actionText;

        private BaseItemConfig _targetItemConfig;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Start()
        {
            _actionButton.onClick.AddListener(OnActionButton);
            _removeButton.onClick.AddListener(OnRemoveButton);
        }

        public void Show(BaseItemConfig itemConfig)
        {
            gameObject.SetActive(true);
            _targetItemConfig = itemConfig;

            _removeButton.gameObject.SetActive(_targetItemConfig.CanRemove);

            switch (_targetItemConfig.ItemType)
            {
                case ItemType.Equip:
                    _actionText.text = "Equip";
                    break;
                case ItemType.Usable:
                    _actionText.text = "Use";
                    break;
            }
        }

        public void Hide()
        {
            _targetItemConfig = null;
            gameObject.SetActive(false);
        }

        private void OnActionButton()
        {
            switch (_targetItemConfig.ItemType)
            {
                case ItemType.Equip:
                    _inventory.EquipItem(_targetItemConfig.ID);
                    break;
                case ItemType.Usable:
                    _inventory.UseItem(_targetItemConfig.ID);
                    break;
            }
            
            Hide();
        }

        private void OnRemoveButton()
        {
            _inventory.RemoveItem(_targetItemConfig.ID);
            Hide();
        }
    }
}