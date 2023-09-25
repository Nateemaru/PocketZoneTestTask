using System;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Game
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 40f;
        private float _timeToDestroy = 0;
        private float _currentReachDistance = 0;
        private Timer _destroyTimer;

        private void Update()
        {
            if (_timeToDestroy != 0)
            {
                _currentReachDistance += _speed * Time.deltaTime;

                if (_currentReachDistance >= _timeToDestroy)
                {
                    Destroy(gameObject);
                }
            
                transform.Translate(transform.right * (_speed * Time.deltaTime), Space.World);
            }
        }

        public void SetMaxDistance(float maxDistance) => _timeToDestroy = maxDistance;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.ApplyDamage(2);
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.ApplyDamage(2);
                Destroy(gameObject);
            }
        }
    }
}