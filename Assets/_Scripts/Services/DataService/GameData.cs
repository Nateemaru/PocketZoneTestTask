using System;
using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using UnityEngine;

namespace _Scripts.Services.DataService
{
    [Serializable]
    public class GameData
    {
        public List<InventorySlot> Slots = new List<InventorySlot>();
        public bool IsFirstLaunch = true;
    }
}