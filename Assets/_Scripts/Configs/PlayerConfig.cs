using System;
using _Scripts.Game.PlayerCore;
using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(menuName = "SO/PlayerConfig", fileName = "PlayerConfig")]
    [Serializable]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _damage;
        [SerializeField] private float _hp;

        public PlayerStats PlayerStats => new PlayerStats(_speed, _attackDistance, _hp, _damage);
    }
}