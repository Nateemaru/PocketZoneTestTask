using System;
using System.Collections;
using _Scripts.Configs;
using _Scripts.Factories;
using _Scripts.Game.InventorySystem;
using _Scripts.Services.DataService;
using _Scripts.Services.InputService;
using _Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    public class PlayerShootment : MonoBehaviour, IAmmoHandler
    {
        [SerializeField] private Transform _firePoint;

        private Timer _shootingTimer;
        private BaseInputService _inputService;
        private GameObjectFactory _gameObjectFactory;
        private WeaponItemConfig _weaponConfig;
        private int _bulletsInHolder;
        private IDataReader _dataReader;

        public bool IsReloading { get; private set; }
        public int BulletsInHolder => _bulletsInHolder;
        public int MaxBulletsInHolder => _weaponConfig.HolderCapacity;

        [Inject]
        private void Construct(BaseInputService inputService, GameObjectFactory gameObjectFactory, IDataReader dataReader)
        {
            _inputService = inputService;
            _gameObjectFactory = gameObjectFactory;
            _dataReader = dataReader;
        }

        private void Update()
        {
            if (_weaponConfig != null)
            {
                _shootingTimer.UpdateTimer();

                if (_bulletsInHolder <= 0)
                {
                    StartCoroutine(Reload());
                    return;
                }
            
                if(_inputService.IsFireButton() && _shootingTimer.IsOver)
                {
                    Shoot();
                    _shootingTimer.ResetTimer();
                    _bulletsInHolder--;
                    _dataReader.GetData().PlayerInfo.BulletsInHolder = _bulletsInHolder;
                    _dataReader.SaveData();
                }
            }
        }

        private IEnumerator Reload()
        {
            if (!IsReloading)
            {
                IsReloading = true;
                yield return new WaitForSeconds(_weaponConfig.ReloadingDuration);
                _bulletsInHolder = _weaponConfig.HolderCapacity;
                IsReloading = false;
            }
        }

        private void Shoot()
        {
            GameObject gameObjectPrefab = _gameObjectFactory.CreateGameObject(_weaponConfig.BulletPrefab);
            gameObjectPrefab.transform.position = _firePoint.position;
            gameObjectPrefab.transform.rotation = _firePoint.rotation;
            if (gameObjectPrefab.TryGetComponent(out Bullet bullet))
            {
                bullet.SetMaxDistance(_dataReader.GetData().PlayerInfo.PlayerStats.AttackDistance);
                Debug.Log(_dataReader.GetData().PlayerInfo.PlayerStats.AttackDistance);
            }
        }

        public void SetWeapon(WeaponItemConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _shootingTimer = new Timer(_weaponConfig.ShootingRate, true);
            _bulletsInHolder = _weaponConfig.HolderCapacity;
        }
    }
}