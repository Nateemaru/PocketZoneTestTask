using System;
using UnityEngine;

namespace _Scripts.Game
{
    public class UnitHealth : HealthComponent
    {
        [SerializeField] private BlinkTween _blinkTween;
        
        private Rigidbody2D _rb;

        private void Start()
        {
            if (TryGetComponent(out Rigidbody2D rigidbody))
                _rb = rigidbody;
        }

        public override void Initialize(float value)
        {
            _maxHp = value;
            _currentHp = value;
            OnHealthChanged?.Invoke();
        }

        public override void ApplyDamage(float damage, Action callback = null)
        {
            _currentHp -= damage;
        
            callback?.Invoke();
            OnHealthChanged?.Invoke();

            if (_currentHp > 0)
            {
                _blinkTween.Kill();
                _blinkTween.Play();
                _hasBeenDamaged = true;
            }
            else if(!_isDead)
            {
                Die();
            }
        }
    
        public void ApplyDamageAndPush(float damage, Vector2 attackDirection, Action callback = null)
        {
            ApplyDamage(damage, callback);
            _rb.velocity += attackDirection.normalized * 3f;
        }

        public override void Kill(Action callback = null)
        {
            Die();
        }

        private void Die()
        {
            OnDeadAction?.Invoke();
            _isDead = true;
            OnDeadAction = null;
            OnHealthChanged = null;
        }

        public override bool HasBeenDamaged()
        {
            if (_hasBeenDamaged)
            {
                _hasBeenDamaged = false;
                return true;
            }

            return false;
        }
    }
}