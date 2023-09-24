using System;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using _Scripts.Services.InputService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private Transform _headPlace;
        [SerializeField] private Transform[] _legsPlace;
        [SerializeField] private Transform[] _handsPlace;
        [SerializeField] private Transform _bodyPlace;
        [SerializeField] private Transform _weaponPlace;

        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Start()
        {
            _inventory.OnEquipmentChange += () =>
            {
                EquipHead();
                EquipBody();
                EquipLegs();
                EquipHands();
                EquipWeapon();
            };
        }

        private void Equip(BaseItemConfig itemConfig, Transform container)
        {
            if (itemConfig != null && container != null)
            {
                GameObject weaponModel = Instantiate(itemConfig.Prefab, container.position, container.rotation);
                weaponModel.transform.parent = container;
            }
        }

        private void ClearIfExistArray(Transform[] containers)
        {
            for (int i = 0; i < containers.Length; i++)
            {
                ClearIfExist(containers[i]);
            }
        }
        
        private void ClearIfExist(Transform container)
        {
            if (container.childCount > 0)
            {
                for (int i = 0; i < container.childCount; i++)
                {
                    Destroy(container.GetChild(i).gameObject);
                }
            }
        }

        private void EquipHead()
        {
            ClearIfExist(_headPlace);
            Equip(_inventory.GetEquip<BaseItemConfig>(EquipType.Head), _headPlace);
        }

        private void EquipLegs()
        {
            ClearIfExistArray(_legsPlace);
            for (int i = 0; i < _legsPlace.Length; i++)
            {
                Equip(_inventory.GetEquip<BaseItemConfig>(EquipType.Legs), _legsPlace[i]);
            }
        }

        private void EquipBody()
        {
            ClearIfExist(_bodyPlace);
            Equip(_inventory.GetEquip<BaseItemConfig>(EquipType.Body), _bodyPlace);
        }

        private void EquipHands()
        {
            ClearIfExistArray(_handsPlace);
            for (int i = 0; i < _handsPlace.Length; i++)
            {
                Equip(_inventory.GetEquip<BaseItemConfig>(EquipType.Hands), _handsPlace[i]);
            }
        }

        private void EquipWeapon()
        {
            ClearIfExist(_weaponPlace);
            Equip(_inventory.GetEquip<BaseItemConfig>(EquipType.Weapon), _weaponPlace);
        }
    }
}