using UnityEngine;

namespace _Root.Code.CarFeature
{
    public class CarView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    }
}