using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Button _button;
        
        public bool IsPressed { get; private set; }

        protected virtual void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public abstract void OnClick();
        
        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }
    }
}