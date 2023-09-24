using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventoryContextMenu _contextMenu;
        
        private List<InventorySlotView> _slotViews = new List<InventorySlotView>();
        private ItemsContainerConfig _itemsContainerConfig;

        public List<InventorySlotView> SlotViews => _slotViews;
        public event Action OnInitialized;

        [Inject]
        private void Construct(ItemsContainerConfig itemsContainerConfig)
        {
            _itemsContainerConfig = itemsContainerConfig;
        }

        public void Init()
        {
            _slotViews = GetComponentsInChildren<InventorySlotView>().ToList();
            OnInitialized?.Invoke();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            transform.DOScale(new Vector3(1, 1, 1), 0.25f);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _contextMenu.Hide();
        }

        public void DisplaySlots(InventorySlot[] slots)
        {
            if (_slotViews == null || _slotViews.Count <= 0)
            {
                _slotViews = GetComponentsInChildren<InventorySlotView>().ToList();
            }
            
            foreach (InventorySlotView slotView in _slotViews)
            {
                slotView.ClearData();
            }
            
            foreach (InventorySlot slot in slots)
            {
                BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == slot.ItemID);
                string amount = slot.Count <= 1 ? "" : slot.Count.ToString();
                InventorySlotView slotView = _slotViews.First(view => !view.IsReserved);
                slotView.SetData(itemConfig.ID, itemConfig.Icon, amount);
                slotView.OnClicked += OnSlotViewClicked;
            }
        }

        private void OnSlotViewClicked(string linkedItemID, InventorySlotView slotView)
        {
            BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == linkedItemID);
            _contextMenu.gameObject.SetActive(true);
            _contextMenu.Show(itemConfig);
            _contextMenu.transform.position = slotView.transform.position;
        }
    }
}