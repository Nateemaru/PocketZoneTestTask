using System;
using UnityEngine;

namespace _Scripts.Utilities
{
    public class Timer
    {
        private float _duration;
        private float _timer;
        private bool _isOver;
        
        public bool IsOver => _isOver;

        public Timer(float duration, bool isOverOnStart = false)
        {
            _duration = duration;

            if (isOverOnStart)
            {
                _isOver = true;
            }
            else
            {
                ResetTimer();
            }
        }

        public void UpdateTimer()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                _isOver = true;
            }
        }

        public void ResetTimer()
        {
            _isOver = false;
            _timer = _duration;
        }
    }
}