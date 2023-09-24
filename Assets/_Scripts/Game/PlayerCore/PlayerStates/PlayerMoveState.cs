using _Scripts.Services.StateMachines;

namespace _Scripts.Game.PlayerCore.PlayerStates
{
    public class PlayerMoveState : IState
    {
        private PlayerController _context;

        public PlayerMoveState(PlayerController context)
        {
            _context = context;
        }
    }
}