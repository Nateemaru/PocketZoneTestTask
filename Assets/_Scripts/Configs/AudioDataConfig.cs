using System;
using System.Collections.Generic;
using _Scripts.Services.AudioService;
using UnityEngine;

namespace _Scripts.SO
{
    [Serializable]
    public enum SoundType
    {
        Sfx,
        UI,
        Background,
    }
    
    [CreateAssetMenu(fileName = "AudioDataConfig", menuName = "SO/Audio Data Config")]
    public class AudioDataConfig : ScriptableObject
    {
        [SerializeField] private SoundType _type;
        [SerializeField] private List<Sound> _sounds = new List<Sound>();

        public List<Sound> Sounds => _sounds;
        public SoundType Type => _type;
    }
}