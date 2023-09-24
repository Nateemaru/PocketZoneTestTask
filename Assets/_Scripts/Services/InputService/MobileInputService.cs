using System;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Services.InputService
{
    public class MobileInputService : BaseInputService
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private BaseButton _fireButton;
        [SerializeField] private BaseButton _inventoryButton;
        private Vector3 _direction;
        
        private void Start()
        {
            _direction = Vector3.zero;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _direction = new Vector3(_joystick.Direction.x, _joystick.Direction.y);
            }
            else
            {
                _direction = Vector3.zero;
            }
        }

        private void OnDisable()
        {
            _direction = Vector3.zero;
        }
        
        public override Vector3 GetDirection()
        {
            return _direction;
        }

        public override bool IsFireButton()
        {
            return _fireButton.IsPressed;
        }

        public override bool IsInventoryButton()
        {
            return _inventoryButton.IsPressed;
        }
    }
}