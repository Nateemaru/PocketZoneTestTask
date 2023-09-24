using System;
using UnityEngine;

namespace _Scripts.Game
{
    public class Bullet : MonoBehaviour
    {
        private float _speed = 40f;

        private void Update()
        {
            transform.Translate(transform.right * (_speed * Time.deltaTime), Space.World);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.ApplyDamage(2);
                Destroy(gameObject);
            }
        }
    }
}