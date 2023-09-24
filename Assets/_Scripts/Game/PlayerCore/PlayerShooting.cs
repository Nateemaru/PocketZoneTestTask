using System;
using System.Collections;
using _Scripts.Configs;
using _Scripts.Factories;
using _Scripts.Game.InventorySystem;
using _Scripts.Services.InputService;
using _Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        
        private Timer _shootingTimer;
        private BaseInputService _inputService;
        private GameObjectFactory _gameObjectFactory;
        private WeaponItemConfig _weaponConfig;
        private int _holderBulletsLeft;
        public bool IsReloading { get; private set; }

        [Inject]
        private void Construct(BaseInputService inputService, GameObjectFactory gameObjectFactory)
        {
            _inputService = inputService;
            _gameObjectFactory = gameObjectFactory;
        }
        
        private void Update()
        {
            if (_weaponConfig != null)
            {
                _shootingTimer.UpdateTimer();

                if (_holderBulletsLeft <= 0)
                {
                    StartCoroutine(Reload());
                    return;
                }
            
                if(_inputService.IsFireButton() && _shootingTimer.IsOver)
                {
                    Shoot();
                    _shootingTimer.ResetTimer();
                    _holderBulletsLeft--;
                }
            }
        }

        private IEnumerator Reload()
        {
            if (!IsReloading)
            {
                IsReloading = true;
                yield return new WaitForSeconds(2);
                _holderBulletsLeft = _weaponConfig.HolderCapacity;
                IsReloading = false;
            }
        }

        private void Shoot()
        {
            GameObject gameObjectPrefab = _gameObjectFactory.CreateGameObject(_weaponConfig.BulletPrefab);
            gameObjectPrefab.transform.position = _firePoint.position;
            gameObjectPrefab.transform.rotation = _firePoint.rotation;
        }

        public void SetWeapon(WeaponItemConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
            _shootingTimer = new Timer(_weaponConfig.ShootingRate, true);
            _holderBulletsLeft = _weaponConfig.HolderCapacity;
        }
    }
}