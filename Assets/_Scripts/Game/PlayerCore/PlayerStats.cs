using System;

namespace _Scripts.Game.PlayerCore
{
    [Serializable]
    public class PlayerStats
    {
        public int Damage;
        public float Speed;
        public float AttackDistance;
        public float Health;

        public PlayerStats(float speed, float attackDistance, float health, int damage)
        {
            Damage = damage;
            Speed = speed;
            AttackDistance = attackDistance;
            Health = health;
        }
    }
}