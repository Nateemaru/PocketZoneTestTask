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
        private float _speed;

        public EnemyMoveState(BaseEnemy context, ITarget target, float speed)
        {
            _context = context;
            _target = target;
            _speed = speed;
        }

        public void Update()
        {
            Vector2 direction = _target.GetTransform().position - _context.transform.position;
            _context.transform.Translate(direction.normalized * (_speed * Time.deltaTime), Space.World);
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