using System;
using UnityEngine;

namespace Player
{
    public class Input : MonoBehaviour
    {
        [SerializeField] private VariableJoystick _joystick;

        public event Action<Vector3> JoystickPushed;

        public event Action JoystickPulled;

        private void Update()
        {
            Vector3 newDirection = new Vector3(_joystick.Horizontal, transform.position.y, _joystick.Vertical);

            if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
            {
                JoystickPushed?.Invoke(newDirection);
                return;
            }

            JoystickPulled?.Invoke();
        }
    }
}