using System;
using _Scripts.Services.InputService;
using UnityEngine;
using Zenject;

namespace _Scripts.Game.PlayerCore
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMoving : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _hand;

        private Rigidbody2D _rb;
        private BaseInputService _inputService;
        private Vector3 _lookDirection;

        public bool IsMoving { get; private set; }
        public bool IsFacingRight { get; private set; }

        [Inject]
        private void Construct(BaseInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            IsMoving = _inputService.GetDirection() != Vector3.zero;

            if (IsMoving)
            {
                Vector3 direction = _lookDirection == Vector3.zero ? _inputService.GetDirection() : _lookDirection;
                RotateTowards(direction);
            }
            else
            {
                IsMoving = false;
                _rb.velocity = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {
            if (IsMoving)
            {
                Move(_inputService.GetDirection());
            }
        }

        private void Move(Vector3 targetDirection)
        {
            _rb.velocity = targetDirection.normalized * _speed;
        }
        
        private void RotateTowards(Vector3 targetDirection)
        {
            float angle = Mathf.Atan2(targetDirection.x, -targetDirection.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _hand.rotation = Quaternion.Slerp(_hand.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            
            switch (targetDirection.x)
            {
                case < 0:
                    if (!IsFacingRight)
                    {
                        transform.Rotate(0, 180, 0);
                        IsFacingRight = true;
                    }
                    break;
                case > 0: 
                    if (IsFacingRight)
                    {
                        transform.Rotate(0, -180, 0);
                        IsFacingRight = false;
                    }
                    break;
            }
        }

        public void SetLookDirection(Vector3 lookDirection) => _lookDirection = lookDirection;
    }
}