using System;
using _Scripts.Game;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Image _mainImage;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _animationDuration;

        private void Start()
        {
            _health.OnDeadAction += () => gameObject.SetActive(false);
            _health.OnHealthChanged += UpdateHealthBar;
        }

        private void LateUpdate()
        {
            transform.LookAt(new Vector3(transform.position.x, Camera.main.transform.transform.position.y,
                Camera.main.transform.position.z));
            transform.Rotate(0, 180, 0);
        }

        private void OnDisable()
        {
            if (_backgroundImage != null)
                _backgroundImage.DOKill(true);
        }

        private void UpdateHealthBar()
        {
            UpdateValue();
            _mainImage.color = _gradient.Evaluate(_health.CurrentHp / _health.MaxHp);
            AnimateBar();
        }

        private void UpdateValue()
        {
            _mainImage.fillAmount = _health.CurrentHp / _health.MaxHp;
            _text.text = _health.CurrentHp.ToString();
        }

        private void AnimateBar()
        {
            _backgroundImage.DOKill();
            _backgroundImage.DOFillAmount(_mainImage.fillAmount, _animationDuration);
        }
    }
}