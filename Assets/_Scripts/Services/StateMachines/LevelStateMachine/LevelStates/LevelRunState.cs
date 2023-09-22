using Zenject;

namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelRunState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;

        public LevelRunState(ILevelStateMachine levelStateMachine)
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