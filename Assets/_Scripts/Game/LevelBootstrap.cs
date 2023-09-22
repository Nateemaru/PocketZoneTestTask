using System;
using _Scripts.Services.DataService;
using _Scripts.Services.PauseService;
using _Scripts.Services.StateMachines.LevelStateMachine;
using _Scripts.Services.StateMachines.LevelStateMachine.LevelStates;
using UnityEngine;
using Zenject;

namespace _Scripts.Game
{
    public class LevelBootstrap : MonoBehaviour
    {
        private ILevelStateMachine _levelStateMachine;
        private IDataReader _dataReader;
        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(
            ILevelStateMachine levelStateMachine,
            IDataReader dataReader,
            PauseHandler pauseHandler)
        {
            _levelStateMachine = levelStateMachine;
            _dataReader = dataReader;
            _pauseHandler = pauseHandler;
        }

        private void Start()
        {
            InitLevelStates();
            _levelStateMachine.ChangeState<LevelStartState>();
        }

        private void InitLevelStates()
        {
            _levelStateMachine.RegisterState(new LevelStartState(_levelStateMachine));
            _levelStateMachine.RegisterState(new LevelRunState(_levelStateMachine));
            _levelStateMachine.RegisterState(new LevelWinState());
            _levelStateMachine.RegisterState(new LevelLoseState());
            _levelStateMachine.RegisterState(new LevelPauseState(_levelStateMachine, _pauseHandler));
        }
    }
}