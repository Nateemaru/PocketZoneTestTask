using _Scripts.Configs;
using _Scripts.Game.AI;
using _Scripts.Game.InventorySystem;
using _Scripts.Services.DataService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    [RequireComponent(typeof(PlayerMoving))]
    [RequireComponent(typeof(PlayerShooting))]
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerController : MonoBehaviour, ITarget
    {
        private PlayerMoving _playerMoving;
        private PlayerShooting _playerShooting;
        private HealthComponent _healthComponent;
        private Inventory _inventory;
        private IDataReader _dataReader;

        public bool IsDead { get; private set; }

        [Inject]
        private void Construct(Inventory inventory, IDataReader dataReader)
        {
            _inventory = inventory;
            _dataReader = dataReader;
        }

        private void Start()
        {
            _playerMoving = GetComponent<PlayerMoving>();
            _playerShooting = GetComponent<PlayerShooting>();
            _healthComponent = GetComponent<HealthComponent>();
            _inventory.OnEquipmentChange += () =>
            {
                _playerShooting.SetWeapon(_inventory.GetEquip<WeaponItemConfig>(EquipType.Weapon));
            };
            
            _healthComponent.Initialize(_dataReader.GetData().PlayerInfo.HP);
            _healthComponent.OnHealthChanged += () =>
            {
                _dataReader.GetData().PlayerInfo.HP = _healthComponent.CurrentHp;
                _dataReader.SaveData();
            };
        }

        private void Update()
        {
            CheckAroundEnemies();
        }

        private void CheckAroundEnemies()
        {
            Collider[] results = new Collider[20];
            int size = Physics.OverlapSphereNonAlloc(transform.position, 2.5f, results);
            
            for (int i = 0; i < size; i++)
            {
                if(results[i].TryGetComponent(out BaseEnemy enemy))
                {
                    _playerMoving.SetLookDirection(enemy.transform.position - transform.position);
                    break;
                }
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}