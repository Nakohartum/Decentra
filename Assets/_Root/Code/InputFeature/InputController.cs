using _Root.Code.UpdateFeature;
using UnityEngine;
using UnityEngine.Events;

namespace _Root.Code.InputFeature
{
    public class InputController : IUpdatable
    {
        public UnityEvent<Vector2> OnMove { get; }
        public UnityEvent<TouchSide> OnSteeringWheelRotate { get; }
        public UnityEvent<bool> OnActionButtonPressed { get; }

        public InputController()
        {
            OnMove = new UnityEvent<Vector2>();
            OnSteeringWheelRotate = new UnityEvent<TouchSide>();
            OnActionButtonPressed = new UnityEvent<bool>();
        }

        public void Dispose()
        {
            OnActionButtonPressed.RemoveAllListeners();
            OnMove.RemoveAllListeners();
        }

        public void Update(float deltaTime)
        {
            var side = CalculateSide();
            OnSteeringWheelRotate.Invoke(side);
            OnMove.Invoke(new Vector2(Input.GetAxis(InputStrings.HORIZONTAL), Input.GetAxis(InputStrings.VERTICAL)));
            OnActionButtonPressed.Invoke(Input.GetButton(InputStrings.ACTION));
        }

        private TouchSide CalculateSide()
        {
            if (Input.touchCount == 0)
            {
                return TouchSide.None;
            }
            int width = Screen.width;
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < width * 0.3f)
            {
                return TouchSide.Left;
            }
            else if (touch.position.x > width * 0.7f)
            {
                return TouchSide.Right;
            }

            return TouchSide.Center;
        }
    }

    public enum TouchSide
    {
        None = 0,
        Left = 1,
        Right = 2,
        Center = 3,
    }
}