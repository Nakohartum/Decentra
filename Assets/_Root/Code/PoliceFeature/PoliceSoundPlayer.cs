using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceSoundPlayer
    {
        private AudioSource _audioSource;
        public AudioClip AudioClip;

        public PoliceSoundPlayer(AudioSource audioSource, AudioClip audioClip)
        {
            _audioSource = audioSource;
            this.AudioClip = audioClip;
            audioSource.clip = this.AudioClip;
            audioSource.loop = true;
        }

        public void PlaySound()
        {
            _audioSource.Play();
        }
    }
}