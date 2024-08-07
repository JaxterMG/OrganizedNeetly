using Core.EventBus;
using UnityEngine;
using Zenject;
using Core.EventBus;

namespace Core.SoundSystem.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource[] _audioSources;
        public SoundLibrary _soundLibrary;

        [Inject]
        private EventBus.EventBus _eventBus;

        private bool _isTurnedOffSound; 

        private void  OnEnable()
        {
            _eventBus.Subscribe<string>(BusEventType.PlaySound, RequestSound);
            _eventBus.Subscribe<bool>(BusEventType.ChangeSoundsVolume, ChangeSoundsVolume);
    
            _audioSources = GetComponents<AudioSource>();
        }
        void OnDisable()
        {
            _eventBus.Unsubscribe<string>(BusEventType.PlaySound, RequestSound);
        }

        private void ChangeSoundsVolume(bool isTurnOff)
        {
            _isTurnedOffSound = isTurnOff;
        }
        public void RequestSound(string soundName)
        {
            if(_isTurnedOffSound) return;
            
            var source = ChooseUnoccupiedSource();
            var clip = FindClip(soundName);
            PlaySound(source, clip);
        }

        private AudioSource ChooseUnoccupiedSource()
        {
            foreach (var source in _audioSources)
            {
                if (!source.isPlaying) return source;
            }

            var newSource = gameObject.AddComponent<AudioSource>();
            AudioSource[] temp = new AudioSource[_audioSources.Length + 1];

            for (int i = 0; i < _audioSources.Length; i++)
            {
                temp[i] = _audioSources[i];
            }

            temp[temp.Length - 1] = newSource;

            _audioSources = temp;
            return newSource;
        }

        private AudioClip FindClip(string soundName)
        {
            _soundLibrary._soundLibrary.TryGetValue(soundName, out var clip);
            return clip;
        }

        private void PlaySound(AudioSource source, AudioClip clip)
        {
            source.PlayOneShot(clip);
        }
    }
}
