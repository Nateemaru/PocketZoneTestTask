using _Scripts.Services.StateMachines;

namespace _Scripts.Game.AI.EnemyStates
{
    public class EnemyIdleState : IState
    {
        private BaseEnemy _context;

        public EnemyIdleState(BaseEnemy context)
        {
            _context = context;
        }
    }
}