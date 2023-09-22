using System;
using System.Linq;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Services.AudioService
{
    public class AudioService : MonoBehaviour
    {
        private static AudioService _instance;
        private AudioStorage[] _audioStorages;
        private AudioMixer _mixer;
        private float _volume;

        public float Volume => _volume;

        public static AudioService Instance => _instance;

        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            
            _mixer = Resources.Load<AudioMixer>("AudioMixer");
            _mixer.GetFloat("Volume", out _volume);
            _audioStorages = GetComponentsInChildren<AudioStorage>();

            foreach (AudioStorage audioStorage in _audioStorages)
            {
                switch (audioStorage.AudioDataConfig.Type)
                {
                    case SoundType.Background: 
                        audioStorage.InitStorage(FindSubgroup("Background"));
                        break;
                    case SoundType.Sfx: 
                        audioStorage.InitStorage(FindSubgroup("Sfx"));
                        break;
                    case SoundType.UI: 
                        audioStorage.InitStorage(FindSubgroup("UI"));
                        break;
                }
            }
        }

        public void SwitchSnapshot(string snapshotName, float duration)
        {
            _mixer.FindSnapshot(snapshotName).TransitionTo(duration);
        }

        public void SetVolume(float volumeLevel)
        {
            _mixer.SetFloat("Volume", Mathf.Lerp(-80, 0, volumeLevel));
            _mixer.GetFloat("Volume", out _volume);
        }

        private AudioMixerGroup FindSubgroup(string subgroupName)
        {
            AudioMixerGroup[] groups = _mixer.FindMatchingGroups(string.Empty);
    
            foreach (AudioMixerGroup group in groups)
            {
                AudioMixerGroup[] subGroups = group.audioMixer.FindMatchingGroups(group.name);
                
                foreach (AudioMixerGroup subGroup in subGroups)
                {
                    if (subGroup.name == subgroupName)
                        return subGroup;
                }
            }
            return null;
        }

        public void Play(string soundName)
        {
            foreach (AudioStorage audioStorage in _audioStorages)
            {
                Sound sound = audioStorage.AudioDataConfig.Sounds.FirstOrDefault(sound => sound.Name == soundName);

                if (sound == null)
                {
                    continue;
                }
                
                sound.Source.Play();
                return;
            }
        }
    }
}