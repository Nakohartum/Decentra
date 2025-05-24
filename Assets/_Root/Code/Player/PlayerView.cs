using System;
using UnityEngine;

namespace _Root.Code
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    }
}