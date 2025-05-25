using System;
using UnityEngine;

namespace _Root.Code.PoliceFeature
{
    public class PoliceView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        
        public event Action OnSoundPlay = delegate { };

        public void PlaySound()
        {
            OnSoundPlay();
        }
    }
}