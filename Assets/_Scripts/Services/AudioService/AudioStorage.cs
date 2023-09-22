using _Scripts.SO;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Services.AudioService
{
    public class AudioStorage : MonoBehaviour
    {
        [SerializeField] private AudioDataConfig _audioDataConfig;

        public AudioDataConfig AudioDataConfig => _audioDataConfig;

        public void InitStorage(AudioMixerGroup audioMixerGroup)
        {
            foreach (Sound sound in _audioDataConfig.Sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.outputAudioMixerGroup = audioMixerGroup;
                
                if(sound.PlayOnAwake)
                {
                    sound.Source.Play();
                }
            }
        }
    }
}