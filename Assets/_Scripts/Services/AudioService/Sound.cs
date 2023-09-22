using System;
using UnityEngine;

namespace _Scripts.Services.AudioService
{
    [Serializable]
    public class Sound
    {
        
        [SerializeField] private AudioClip _clip;
        [SerializeField] private string _name;
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private bool _isLoop;
        [SerializeField] private bool _isRandomPitch;

        private AudioSource _source;

        public AudioClip Clip => _clip;
        public string Name => _name;
        public bool IsLoop => _isLoop;
        public bool IsRandomPitch => _isRandomPitch;
        public bool PlayOnAwake => _playOnAwake;

        public AudioSource Source
        {
            get => _source;
            
            set
            {
                value.clip = _clip;
                value.playOnAwake = _playOnAwake;
                value.loop = _isLoop;
                _source = value;
            }
        }
    }
}