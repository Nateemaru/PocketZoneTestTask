using System;

namespace _Scripts.Game.PlayerCore
{
    public interface IAmmoHandler
    {
        public bool IsReloading { get;}
        public int BulletsInHolder { get; }
        public int MaxBulletsInHolder { get; }
    }
}