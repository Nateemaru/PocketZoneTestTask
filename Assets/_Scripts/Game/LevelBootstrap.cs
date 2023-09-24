using _Scripts.Game.InventorySystem;
using _Scripts.Services.DataService;
using _Scripts.Services.StateMachines.LevelStateMachine;
using _Scripts.Services.StateMachines.LevelStateMachine.LevelStates;
using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class LevelBootstrap : MonoBehaviour
    {
        private ILevelStateMachine _levelStateMachine;
        private IDataReader _dataReader;
        private Inventory _inventory;
        private InventoryView _inventoryView;
        private EquipmentInventoryView _equipmentInventoryView;

        [Inject]
        private void Construct(
            ILevelStateMachine levelStateMachine,
            IDataReader dataReader,
            Inventory inventory,
            InventoryView inventoryView,
            EquipmentInventoryView equipmentInventoryView)
        {
            _inventory = inventory;
            _levelStateMachine = levelStateMachine;
            _dataReader = dataReader;
            _inventoryView = inventoryView;
            _equipmentInventoryView = equipmentInventoryView;
        }

        private void Start()
        {
            InitLevelStates();
            _inventoryView.Init();
            _equipmentInventoryView.Init();
            _levelStateMachine.ChangeState<LevelStartState>();
        }

        private void InitLevelStates()
        {
            _levelStateMachine.RegisterState(new LevelStartState(_levelStateMachine));
            _levelStateMachine.RegisterState(new LevelRunState(_levelStateMachine));
            _levelStateMachine.RegisterState(new LevelWinState());
            _levelStateMachine.RegisterState(new LevelLoseState());
            _levelStateMachine.RegisterState(new LevelPauseState(_levelStateMachine));
        }
    }
}