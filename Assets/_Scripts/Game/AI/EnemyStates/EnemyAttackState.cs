using _Scripts.Game.PlayerCore;
using _Scripts.Services.StateMachines;

namespace _Scripts.Game.AI.EnemyStates
{
    public class EnemyAttackState : IState
    {
        private BaseEnemy _context;
        private ITarget _target;

        public EnemyAttackState(BaseEnemy context, ITarget target)
        {
            _context = context;
            _target = target;
        }
    }
}