using _Scripts.Services.StateMachines;

namespace _Scripts.Game.PlayerCore.PlayerStates
{
    public class PlayerMoveAttackState : IState
    {
        private PlayerController _context;

        public PlayerMoveAttackState(PlayerController context)
        {
            _context = context;
        }
    }
}