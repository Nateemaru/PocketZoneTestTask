using System;
using _Scripts.Game.PlayerCore;
using _Scripts.Services;
using _Scripts.Services.StateMachines;
using _Scripts.Utilities;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.AI
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _attackRate;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _attackDistance;
        [SerializeField] protected float _fov;
        [SerializeField] protected float _hp;
        [SerializeField] protected float _provokeDistance;
        
        protected HealthComponent _health;
        protected EnemiesHasher _enemiesHasher;
        protected ITarget _target;
        protected UnitStateMachine _stateMachine;
        protected bool _isProvoked;
        private DropHandler _dropHandler;

        public event Action OnDead;

        [Inject]
        private void Construct(ITarget target, EnemiesHasher enemiesHasher, DropHandler dropHandler)
        {
            _dropHandler = dropHandler;
            _enemiesHasher = enemiesHasher;
            _target = target;
            _enemiesHasher.Add(this);
        }
        
        protected virtual void Start()
        {
            _health = GetComponent<HealthComponent>();
            _health.Initialize(_hp, _hp);
            _health.OnHealthChanged += () => _isProvoked = true;
            _health.OnDeadAction += () =>
            {
                _enemiesHasher.Remove(this);
                OnDead?.Invoke();
                _dropHandler.Drop(transform.position);
                Destroy(gameObject);
            };
        }

        protected virtual void OnEnable()
        {
            _enemiesHasher.Add(this);
        }

        private void Update()
        {
            if (!_target.IsDead)
            {
                if (transform.IsTargetNearby(_target.GetTransform(), _provokeDistance))
                    _isProvoked = true;

                if (Vector3.Distance(transform.position, _target.GetTransform().position) > _provokeDistance * 2)
                    _isProvoked = false;
            }

            _stateMachine?.UpdateMachine();
        }

        public class Factory : PlaceholderFactory<GameObject, BaseEnemy>
        {
            
        }
    }
}