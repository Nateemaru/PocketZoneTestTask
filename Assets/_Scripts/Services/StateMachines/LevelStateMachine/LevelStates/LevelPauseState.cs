using _Scripts.Services.PauseService;
using Zenject;

namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelPauseState : IState
    {
        private readonly ILevelStateMachine _levelStateMachine;
        private PauseHandler _pauseHandler;

        public LevelPauseState(ILevelStateMachine levelStateMachine,PauseHandler pauseHandler)
        {
            _levelStateMachine = levelStateMachine;
            _pauseHandler = pauseHandler;
        }

        public void Enter()
        {
            _pauseHandler.SetPause(true);
        }

        public void Exit()
        {
            _pauseHandler.SetPause(false);
        }
    }
}