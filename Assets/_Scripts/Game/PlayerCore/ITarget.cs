using UnityEngine;

namespace _Scripts.Game.PlayerCore
{
    public interface ITarget
    {
        public Transform GetTransform();
        public bool IsDead { get;}
    }
}