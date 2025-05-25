using System.Collections;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarSoundPlayer
    {
        private AudioSource _audioSource;
        public AudioClip EngineSounds;
        public AudioClip CarOpenSounds;
        public Coroutine SoundRoutine;

        public CarSoundPlayer(AudioSource audioSource, AudioClip engineSounds, AudioClip carOpenSounds)
        {
            _audioSource = audioSource;
            EngineSounds = engineSounds;
            CarOpenSounds = carOpenSounds;
            _audioSource.clip = EngineSounds;
            _audioSource.loop = true;
        }

        public void PlayOpenCarSound()
        {
            _audioSource.PlayOneShot(CarOpenSounds);
        }
        
        public void PlayCarSound()
        {
            _audioSource.clip = EngineSounds;
            _audioSource.Play();
        }

        public IEnumerator DecreaseVolumeRoutine()
        {
            for (float i = _audioSource.volume; i > 0.2f; i-=Time.deltaTime)
            {
                _audioSource.volume = i;
                yield return null;
            }
        }
        
        public IEnumerator IncreaseSoundRoutine()
        {
            for (float i = _audioSource.volume; i < 0.4f; i+=Time.deltaTime)
            {
                _audioSource.volume = i;
                yield return null;
            }
        }

        public void StartDecreasingSound(CarView carView)
        {
            if (SoundRoutine != null)
            {
                carView.StopCoroutine(SoundRoutine);
            }
            carView.StartCoroutine(DecreaseVolumeRoutine());
        }
        
        public void StartIncreasingSound(CarView carView)
        {
            if (SoundRoutine != null)
            {
                carView.StopCoroutine(SoundRoutine);
            }
            carView.StartCoroutine(IncreaseSoundRoutine());
        }
    }
}