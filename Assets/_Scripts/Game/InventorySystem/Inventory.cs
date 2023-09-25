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
        private ItemsContainerConfig _itemsContainerConfig;
        private InventoryView _inventoryView;
        private EquipmentInventoryView _equipmentInventoryView;
        private IDataReader _dataReader;
        private List<EquipmentSlot> _equipmentSlots = new List<EquipmentSlot>();

        public event Action OnEquipmentChange;

        public Inventory(
            ItemsContainerConfig itemsContainerConfig, 
            InventoryView inventoryView, 
            EquipmentInventoryView equipmentInventoryView,
            IDataReader dataReader)
        {
            _itemsContainerConfig = itemsContainerConfig;
            _inventoryView = inventoryView;
            _equipmentInventoryView = equipmentInventoryView;
            _dataReader = dataReader;

            _slots = _dataReader.GetData().Slots;
            _equipmentSlots = _dataReader.GetData().EquipmentSlots;
            _inventoryView.OnInitialized += 
                () => inventoryView.DisplaySlots(_slots.ToArray());
            _equipmentInventoryView.OnInitialized +=
                () =>
                {
                    _equipmentInventoryView.DisplaySlots(_equipmentSlots.ToArray());
                    OnEquipmentChange?.Invoke();
                };
        }

        public TConfig GetItem<TConfig>(string ID) where TConfig : BaseItemConfig
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

            return itemConfig as TConfig;
        }
        
        public TConfig GetEquip<TConfig>(EquipType equipType) where TConfig : BaseItemConfig
        {
            foreach (EquipmentSlot slot in _equipmentSlots)
            {
                if (slot.EquipType == equipType)
                {
                    BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == slot.ItemID);
                    return itemConfig as TConfig;
                }
            }

            return null;
        }

        public void AddItem(string itemID)
        {
            InventorySlot existingSlot = _slots.FirstOrDefault(slot => slot.ItemID == itemID);

            if (existingSlot != null)
            {
                existingSlot.Count++;
            }
            else
            {
                InventorySlot newSlot = new InventorySlot(itemID);
                _slots.Add(newSlot);
            }

            ApplyChanges();
        }

        public void RemoveItem(string itemID)
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemID == itemID)
                {
                    slot.Count--;

                    if (slot.Count < 1)
                    {
                        _slots.Remove(slot);
                        break;
                    }
                }
            }
            
            ApplyChanges();
        }

        private void ApplyChanges()
        {
            _dataReader.GetData().Slots = _slots;
            _dataReader.GetData().EquipmentSlots = _equipmentSlots;
            _dataReader.SaveData();
            _inventoryView.DisplaySlots(_slots.ToArray());
        }

        public void EquipItem(string itemID)
        {
            BaseItemConfig itemConfig = _itemsContainerConfig.ItemsConfigs.First(item => item.ID == itemID);
            RemoveItem(itemID);
            _equipmentInventoryView.SetEquip(itemConfig, itemConfig.EquipType);
            EquipmentSlot equipmentSlot = _equipmentSlots.Find(item => item.EquipType == itemConfig.EquipType);
            
            if (equipmentSlot != null)
            {
                InventorySlot existingSlot = _slots.FirstOrDefault(slot => slot.ItemID == equipmentSlot.ItemID);

                if (existingSlot != null)
                {
                    existingSlot.Count++;
                }
                else
                {
                    InventorySlot newSlot = new InventorySlot(equipmentSlot.ItemID);
                    _slots.Add(newSlot);
                }
                
                _equipmentSlots.Remove(equipmentSlot);
            }
            
            _equipmentSlots.Add(new EquipmentSlot(itemConfig.ID, itemConfig.EquipType));
            OnEquipmentChange?.Invoke();
            ApplyChanges();
        }

        public void UseItem(string itemID)
        {
            
        }
    }
}