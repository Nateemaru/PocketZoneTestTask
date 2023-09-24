using System;

namespace _Scripts.Game.InventorySystem
{
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