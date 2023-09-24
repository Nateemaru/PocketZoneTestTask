using System;
using _Scripts.Configs;
using _Scripts.Factories;
using _Scripts.Game.AI;
using _Scripts.Game.InventorySystem;
using _Scripts.Game.PlayerCore.PlayerStates;
using _Scripts.Services.InputService;
using _Scripts.Services.StateMachines;
using _Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    [RequireComponent(typeof(PlayerMoving))]
    [RequireComponent(typeof(PlayerShooting))]
    public class PlayerController : MonoBehaviour
    {
        private BaseInputService _inputService;
        private PlayerInfo _playerInfo;
        private UnitStateMachine _stateMachine;
        private PlayerMoving _playerMoving;
        private PlayerShooting _playerShooting;
        private Inventory _inventory;

        [Inject]
        private void Construct(BaseInputService inputService, Inventory inventory)
        {
            _inputService = inputService;
            _inventory = inventory;
        }

        private void Start()
        {
            _playerMoving = GetComponent<PlayerMoving>();
            _playerShooting = GetComponent<PlayerShooting>();
            _stateMachine = new UnitStateMachine();
            
            _stateMachine.AddAnyTransition(new PlayerIdleState(this), 
                () => !_inputService.IsFireButton() && !_playerMoving.IsMoving);
            _stateMachine.AddAnyTransition(new PlayerMoveState(this), 
                () => _playerMoving.IsMoving);
            _stateMachine.AddAnyTransition(new PlayerAttackState(this), 
                () => _inputService.IsFireButton());
            _stateMachine.AddAnyTransition(new PlayerMoveAttackState(this), 
                () => _inputService.IsFireButton() && _playerMoving.IsMoving);
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
    }
}