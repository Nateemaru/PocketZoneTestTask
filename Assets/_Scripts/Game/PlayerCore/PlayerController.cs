using System;
using _Scripts.Configs;
using _Scripts.Game.AI;
using _Scripts.Game.InventorySystem;
using _Scripts.Services.DataService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerShootment))]
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerController : MonoBehaviour, ITarget
    {
        [SerializeField] private LayerMask _targetLayer;
        
        private PlayerMovement _playerMoving;
        private PlayerShootment _playerShooting;
        private HealthComponent _healthComponent;
        private Inventory _inventory;
        private IDataReader _dataReader;

        public bool IsDead { get; private set; }
        public event Action OnDead;

        [Inject]
        private void Construct(Inventory inventory, IDataReader dataReader)
        {
            _inventory = inventory;
            _dataReader = dataReader;
        }

        private void Start()
        {
            _playerMoving = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShootment>();
            _healthComponent = GetComponent<HealthComponent>();
            _inventory.OnEquipmentChange += () =>
            {
                _playerShooting.SetWeapon(_inventory.GetEquip<WeaponItemConfig>(EquipType.Weapon));
            };
            
            _healthComponent.Initialize(_dataReader.GetData().PlayerInfo.PlayerStats.Health, 
                _dataReader.GetData().PlayerInfo.CurrentHp);
            _healthComponent.OnHealthChanged += () =>
            {
                _dataReader.GetData().PlayerInfo.CurrentHp = _healthComponent.CurrentHp;
                _dataReader.SaveData();
            };
            _healthComponent.OnDeadAction += () => OnDead?.Invoke();
        }

        private void Update()
        {
            CheckAroundEnemies();
        }

        private void CheckAroundEnemies()
        {
            Collider[] results = new Collider[20];
            int size = Physics.OverlapSphereNonAlloc(transform.position,
                _dataReader.GetData().PlayerInfo.PlayerStats.AttackDistance, results, _targetLayer);
            
            for (int i = 0; i < size; i++)
            {
                if(results[i].TryGetComponent(out BaseEnemy enemy))
                {
                    Debug.Log("Found an enemy");
                    _playerMoving.SetLookDirection(enemy.transform.position - transform.position);
                    break;
                }
            }

            if (size <= 0)
            {
                _playerMoving.SetLookDirection(Vector3.zero);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}