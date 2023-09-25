using _Scripts.Services.DataService;
using _Scripts.Services.SceneLoadService;

namespace _Scripts.Services.StateMachines.LevelStateMachine.LevelStates
{
    public class LevelLoseState : IState
    {
        private ISceneLoadService _sceneLoadService;
        private IDataReader _dataReader;

        public LevelLoseState(IDataReader dataReader, ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _dataReader = dataReader;
        }

        public void Enter()
        {
            _dataReader.GetData().PlayerInfo.CurrentHp = _dataReader.GetData().PlayerInfo.PlayerStats.Health;
            _sceneLoadService.Load(GlobalConstants.GameScene);
        }
    }
}