using System;
using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using _Scripts.Game.PlayerCore;
using UnityEngine;

namespace _Scripts.Services.DataService
{
    [Serializable]
    public class GameData
    {
        public List<InventorySlot> Slots = new List<InventorySlot>();
        public List<EquipmentSlot> EquipmentSlots = new List<EquipmentSlot>();
        public bool IsFirstLaunch = true;
        public PlayerInfo PlayerInfo = new PlayerInfo();
    }
}