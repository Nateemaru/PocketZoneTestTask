using _Scripts.Game.PlayerCore;
using UnityEngine;
using Zenject;

namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelRunState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;
        private PlayerController _playerController;
        private EnemiesHasher _enemiesHasher;

        public LevelRunState(ILevelStateMachine levelStateMachine, PlayerController playerController, EnemiesHasher enemiesHasher)
        {
            _enemiesHasher = enemiesHasher;
            _playerController = playerController;
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            _playerController.OnDead += HandlePlayerDeath;
        }

        public void Exit()
        {
            _playerController.OnDead -= HandlePlayerDeath;
        }
        
        private void HandlePlayerDeath()
        {
            _levelStateMachine.ChangeState<LevelLoseState>();
        }
    }
}