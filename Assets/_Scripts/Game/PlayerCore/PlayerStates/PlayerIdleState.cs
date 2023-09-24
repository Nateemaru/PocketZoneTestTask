using _Scripts.Services.StateMachines;

namespace _Scripts.Game.PlayerCore.PlayerStates
{
    public class PlayerIdleState : IState
    {
        private PlayerController _context;

        public PlayerIdleState(PlayerController context)
        {
            _context = context;
        }
    }
}