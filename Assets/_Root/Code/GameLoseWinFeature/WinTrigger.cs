using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Code.GameLoseWinFeature
{
    public class WinTrigger : MonoBehaviour
    {
        public UnityEvent OnWin  = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnWin.Invoke();
        }
    }
}