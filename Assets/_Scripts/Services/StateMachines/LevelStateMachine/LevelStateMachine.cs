using System;
using System.Collections.Generic;

namespace _Scripts.Services.StateMachines.LevelStateMachine
{
    public class LevelStateMachine : ILevelStateMachine
    {
        private Dictionary<Type, IState> _registeredStates;
        private IState _currentLevelState;

        public IStateMachine.StateChanged OnStateChanged { get; set; }
        
        public IState CurrentState => _currentLevelState;
        public IState LastState { get; private set; }

        public LevelStateMachine()
        {
            _registeredStates = new Dictionary<Type, IState>();
        }

        public void RegisterState<TState>(TState state) where TState : IState =>
            _registeredStates.Add(typeof(TState), state);

        public void ChangeState<TState>() where TState : class, IState
        {
            TState state = GetState<TState>();

            if (state != _currentLevelState)
            {
                _currentLevelState?.Exit();
                LastState = _currentLevelState;
                _currentLevelState = state;
                _currentLevelState.Enter();
                OnStateChanged?.Invoke(_currentLevelState);
            }
        }
    
        private TState GetState<TState>() where TState : class, IState => 
            _registeredStates[typeof(TState)] as TState;
    }
}