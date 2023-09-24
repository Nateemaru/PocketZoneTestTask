using _Scripts.Game.AI.EnemyStates;
using _Scripts.Services.StateMachines;
using _Scripts.Utilities;

namespace _Scripts.Game.AI
{
    public class Zombie : BaseEnemy
    {
        protected override void Start()
        {
            base.Start();
            _stateMachine = new UnitStateMachine();
            _stateMachine.AddAnyTransition(new EnemyIdleState(this), 
                () => _health.CurrentHp > 0 && !_isProvoked);
            _stateMachine.AddAnyTransition(new EnemyMoveState(this, _target), 
                () => _health.CurrentHp > 0 
                      && _isProvoked
                      && (!transform.IsTargetNearby(_target.GetTransform(), _attackDistance) 
                          || !transform.IsTargetInSight(_target.GetTransform(), _fov)));
            _stateMachine.AddAnyTransition(new EnemyAttackState(this, _target),
                () =>
                    _health.CurrentHp > 0
                    && _isProvoked
                    && !_target.IsDead
                    && transform.IsTargetNearby(_target.GetTransform(), _attackDistance)
                    && transform.IsTargetInSight(_target.GetTransform(), _fov));
        }
    }
}