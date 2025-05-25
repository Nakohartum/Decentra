using System.Collections;
using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarSoundPlayer
    {
        private AudioSource AudioSource;
        public AudioClip AudioClip;
        public Coroutine SoundRoutine;

        public CarSoundPlayer(AudioSource audioSource, AudioClip audioClip)
        {
            AudioSource = audioSource;
            AudioClip = audioClip;
            AudioSource.clip = AudioClip;
            AudioSource.loop = true;
        }

        public void PlayCarSound()
        {
            AudioSource.Play();
        }

        public IEnumerator DecreaseVolumeRoutine()
        {
            for (float i = AudioSource.volume; i > 0.2f; i-=Time.deltaTime)
            {
                AudioSource.volume = i;
                yield return null;
            }
        }
        
        public IEnumerator IncreaseSoundRoutine()
        {
            for (float i = AudioSource.volume; i < 0.4f; i+=Time.deltaTime)
            {
                AudioSource.volume = i;
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