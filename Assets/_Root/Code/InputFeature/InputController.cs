using _Root.Code.UpdateFeature;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Code.InputFeature
{
    public class InputController : IUpdatable
    {
        public UnityEvent<Vector2> OnMove { get; }
        public UnityEvent<bool> OnActionButtonPressed { get; }

        public InputController()
        {
            OnMove = new UnityEvent<Vector2>();
            OnActionButtonPressed = new UnityEvent<bool>();
        }

        public void Dispose()
        {
            OnActionButtonPressed.RemoveAllListeners();
            OnMove.RemoveAllListeners();
        }

        public void Update(float deltaTime)
        {
            OnMove.Invoke(new Vector2(Input.GetAxis(InputStrings.HORIZONTAL), Input.GetAxis(InputStrings.VERTICAL)));
            OnActionButtonPressed.Invoke(Input.GetButton(InputStrings.ACTION));
        }
    }
}