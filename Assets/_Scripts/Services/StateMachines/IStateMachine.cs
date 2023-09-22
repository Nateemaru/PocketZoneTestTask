namespace _Scripts.Services.StateMachines
{
    public interface IStateMachine
    {
        public delegate void StateChanged(IState state);
        
        public StateChanged OnStateChanged { get; set; }

        public IState CurrentState { get; }
        public IState LastState { get; }

        public void ChangeState<TState>() where TState : class, IState;
        public void RegisterState<TState>(TState state) where TState : IState;
    }
}