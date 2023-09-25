using System;
using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
using _Scripts.Game.PlayerCore;
using _Scripts.Services;
using _Scripts.Services.DataService;
using _Scripts.Services.SceneLoadService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private BaseItemConfig[] _defaultItems;
        [SerializeField] private PlayerConfig _playerConfig;

        private IDataReader _dataReader;
        private ISceneLoadService _sceneLoadService;

        [Inject]
        private void Construct(IDataReader dataReader, ISceneLoadService sceneLoadService)
        {
            _dataReader = dataReader;
            _sceneLoadService = sceneLoadService;
        }

        private void Start()
        {
            _dataReader.Init();

            if (_dataReader.GetData().IsFirstLaunch)
            {
                HandleFirstLaunch();
            }
            
            _sceneLoadService.Load(GlobalConstants.GameScene);
        }

        private void HandleFirstLaunch()
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.PlayerStats = _playerConfig.PlayerStats;
            playerInfo.CurrentHp = _playerConfig.PlayerStats.Health;
            _dataReader.GetData().PlayerInfo = playerInfo;
            foreach (BaseItemConfig item in _defaultItems)
            {
                if (item.ItemType == ItemType.Equip)
                {
                    EquipmentSlot newSlot = new EquipmentSlot(item.ID, item.EquipType);
                    _dataReader.GetData().EquipmentSlots.Add(newSlot);
                }
                else
                {
                    InventorySlot newSlot = new InventorySlot(item.ID);
                    _dataReader.GetData().Slots.Add(newSlot);
                }
            }

            _dataReader.GetData().IsFirstLaunch = false;
            _dataReader.SaveData();
        }
    }
}