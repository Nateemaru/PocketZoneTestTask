using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Configs;
using _Scripts.Services.DataService;
using _Scripts.UI;

namespace _Scripts.Game.InventorySystem
{
    [Serializable]
    public sealed class Inventory
    {
        private List<InventorySlot> _slots = new List<InventorySlot>();
        public bool WasInitialized { get; private set; }
        private ItemsContainerConfig _itemsContainerConfig;
        private InventoryView _inventoryView;
        private IDataReader _dataReader;

        public event Action OnInitialized;

        public Inventory(ItemsContainerConfig itemsContainerConfig, InventoryView inventoryView, IDataReader dataReader)
        {
            _itemsContainerConfig = itemsContainerConfig;
            _inventoryView = inventoryView;
            _dataReader = dataReader;

            _slots = _dataReader.GetData().Slots;
            _inventoryView.OnInitialized += DisplaySlots;
        }

        private void DisplaySlots()
        {
            foreach (InventorySlot slot in _slots)
            {
                BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == slot.ItemID);
                string amount = slot.Count <= 1 ? "" : slot.Count.ToString();
                _inventoryView.SlotsView.First(view => !view.IsReserved).SetData(itemConfig.Icon, amount);
            }
        }

        private BaseItemConfig GetItem(string ID)
        {
            BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == ID);
            
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemID == ID)
                {
                    slot.Count--;

                    if (slot.Count < 1)
                    {
                        _slots.Remove(slot);
                        break;
                    }
                }
            }

            return itemConfig;
        }

        public void AddItem(string itemID)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemID == itemID)
                {
                    slot.Count++;
                    return;
                }
            }
            
            InventorySlot newSlot = new InventorySlot(itemID);
            _slots.Add(newSlot);
        }

        public void Init(List<InventorySlot> slots = null)
        {
            if (slots != null && slots.Count > 0)
            {
                _slots = slots;
            }

            WasInitialized = true;
            OnInitialized?.Invoke();
        }
    }

    [Serializable]
    public class InventorySlot
    {
        public string ItemID;
        public int Count;

        public InventorySlot(string itemID)
        {
            ItemID = itemID;
            Count = 1;
        }
    }
}