using _Scripts.Game.PlayerCore;
using _Scripts.Services.StateMachines;
using _Scripts.Utilities;

namespace _Scripts.Game.AI.EnemyStates
{
    public class EnemyAttackState : IState
    {
        private BaseEnemy _context;
        private ITarget _target;
        private float _damage;
        private float _attackRate;
        private Timer _attackTimer;
        private float _attackDistance;
        private float _fov;

        public EnemyAttackState(BaseEnemy context, ITarget target, float damage, float attackRate,
            float attackDistance, float fov)
        {
            _fov = fov;
            _attackDistance = attackDistance;
            _attackRate = attackRate;
            _damage = damage;
            _context = context;
            _target = target;
            
            _attackTimer = new Timer(_attackRate, true);
        }

        public void Update()
        {
            _attackTimer.UpdateTimer();
            
            if (_attackTimer.IsOver)
            {
                Attack();
                _attackTimer.ResetTimer();
            }
        }
        
        private void Attack()
        {
            if(_target != null && _context.transform.IsTargetNearby(_target.GetTransform(), _attackDistance) 
                               && _context.transform.IsTargetInSight(_target.GetTransform(), _fov))
            {
                _target.GetTransform().GetComponent<HealthComponent>()
                    .ApplyDamage(_damage);
            }
        }
    }
}