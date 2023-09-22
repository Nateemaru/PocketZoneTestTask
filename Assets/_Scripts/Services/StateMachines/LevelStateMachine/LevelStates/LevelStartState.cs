using Zenject;

namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelStartState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;

        public LevelStartState(ILevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            _levelStateMachine.ChangeState<LevelRunState>();
        }
    }
}