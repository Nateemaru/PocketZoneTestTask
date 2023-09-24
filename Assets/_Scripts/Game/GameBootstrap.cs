using System;
using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.Game.InventorySystem;
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
            if (_dataReader.GetData().Slots.Count <= 0)
            {
                foreach (BaseItemConfig item in _defaultItems)
                {
                    foreach (InventorySlot slot in _dataReader.GetData().Slots)
                    {
                        if (slot.ItemID == item.ID)
                        {
                            slot.Count++;
                            break;
                        }
                    }
                    
                    InventorySlot newSlot = new InventorySlot(item.ID);
                    _dataReader.GetData().Slots.Add(newSlot);
                }
            }

            _dataReader.GetData().IsFirstLaunch = false;
            _dataReader.SaveData();
        }
    }
}