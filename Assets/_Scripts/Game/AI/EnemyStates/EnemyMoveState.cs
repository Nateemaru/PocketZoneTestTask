using _Scripts.Game.PlayerCore;
using _Scripts.Services.StateMachines;
using UnityEngine;

namespace _Scripts.Game.AI.EnemyStates
{
    public class EnemyMoveState : IState
    {
        private BaseEnemy _context;
        private ITarget _target;
        private bool _isRightFacing;

        public EnemyMoveState(BaseEnemy context, ITarget target)
        {
            _context = context;
            _target = target;
        }

        public void Update()
        {
            Vector2 direction = _target.GetTransform().position - _context.transform.position;
            _context.transform.Translate(direction.normalized * (2 * Time.deltaTime), Space.World);
            switch (direction.x)
            {
                case < 0:
                    if (!_isRightFacing)
                    {
                        _context.transform.Rotate(0, 180, 0);
                        _isRightFacing = true;
                    }
                    break;
                case > 0:
                    if (_isRightFacing)
                    {
                        _context.transform.Rotate(0, -180, 0);
                        _isRightFacing = false;
                    }
                    break;
            }
        }
    }
}