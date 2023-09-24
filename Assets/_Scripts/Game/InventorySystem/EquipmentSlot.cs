using System;

namespace _Scripts.Game.InventorySystem
{
    [Serializable]
    public class EquipmentSlot
    {
        public string ItemID;
        public EquipType EquipType;

        public EquipmentSlot(string itemID, EquipType equipType)
        {
            ItemID = itemID;
            EquipType = equipType;
        }
    }
}