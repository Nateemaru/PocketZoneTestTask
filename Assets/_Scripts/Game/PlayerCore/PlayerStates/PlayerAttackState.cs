using _Scripts.Services.StateMachines;

namespace _Scripts.Game.PlayerCore.PlayerStates
{
    public class PlayerAttackState : IState
    {
        private PlayerController _context;

        public PlayerAttackState(PlayerController context)
        {
            _context = context;
        }
    }
}