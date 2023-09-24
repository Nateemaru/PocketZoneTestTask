using System;

namespace _Scripts.Game.PlayerCore
{
    [Serializable]
    public class PlayerInfo
    {
        public float HP = 100f;
        public int XPosition = 0;
        public int YPosition = 0;
        public int BulletsInHolder = 0;
    }
}