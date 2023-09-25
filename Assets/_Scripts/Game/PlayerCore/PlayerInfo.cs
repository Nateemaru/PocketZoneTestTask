using System;

namespace _Scripts.Game.PlayerCore
{
    [Serializable]
    public class PlayerInfo
    {
        public float CurrentHp = 0f;
        public int XPosition = 0;
        public int YPosition = 0;
        public int BulletsInHolder = 0;
        public PlayerStats PlayerStats;
    }
}