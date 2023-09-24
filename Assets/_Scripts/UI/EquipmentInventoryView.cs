using System;
using System.Linq;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class EquipmentInventoryView : MonoBehaviour
    {
        private EquipmentSlotView[] _equipmentSlotsViews;
        private ItemsContainerConfig _itemsContainerConfig;

        public event Action OnInitialized;
        
        [Inject]
        private void Construct(ItemsContainerConfig itemsContainerConfig)
        {
            _itemsContainerConfig = itemsContainerConfig;
        }

        public void Init()
        {
            _equipmentSlotsViews = GetComponentsInChildren<EquipmentSlotView>();
            OnInitialized?.Invoke();
        }

        public void SetEquip(BaseItemConfig itemConfig, EquipType equipType)
        {
            if (_equipmentSlotsViews == null || _equipmentSlotsViews.Length <= 0)
            {
                _equipmentSlotsViews = GetComponentsInChildren<EquipmentSlotView>();
            }
            
            EquipmentSlotView view = _equipmentSlotsViews.First(view => view.EquipType == equipType);
            view.ClearData();
            view.SetData(itemConfig.Icon);
        }
        
        public void DisplaySlots(EquipmentSlot[] slots)
        {
            if (_equipmentSlotsViews == null || _equipmentSlotsViews.Length <= 0)
            {
                _equipmentSlotsViews = GetComponentsInChildren<EquipmentSlotView>();
            }
            
            foreach (EquipmentSlotView equipmentSlotView in _equipmentSlotsViews)
            {
                equipmentSlotView.ClearData();
            }
            
            foreach (EquipmentSlot slot in slots)
            {
                BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == slot.ItemID);
                SetEquip(itemConfig, itemConfig.EquipType);
            }
        }
    }
}