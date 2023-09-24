namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelPauseState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;

        public LevelPauseState(ILevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}