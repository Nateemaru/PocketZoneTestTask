using System;
using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(menuName = "SO/Items/WeaponItemConfig", fileName = "WeaponItemConfig")]
    public class WeaponItemConfig : BaseItemConfig
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private  float _shootingRate;
        [SerializeField] private  int _holderCapacity;

        public GameObject BulletPrefab => _bulletPrefab;
        public float ShootingRate => _shootingRate;
        public int HolderCapacity => _holderCapacity;
    }
}