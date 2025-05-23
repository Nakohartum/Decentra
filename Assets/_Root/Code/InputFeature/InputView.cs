using UnityEngine;
using UnityEngine.UI;

namespace _Root.Code.InputFeature
{
    public class InputView : MonoBehaviour
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public Button ActionButton { get; private set; }
    }
}