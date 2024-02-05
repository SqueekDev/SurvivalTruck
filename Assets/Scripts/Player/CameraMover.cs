using UnityEngine;

namespace Player
{
    public class CameraMover : MonoBehaviour
    {
        private const float MaxInputValue = 1f;

        [SerializeField] private VariableJoystick _floatingJoystick;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _speedXAxis;
        [SerializeField] private float _speedYAxis;
        [SerializeField] private float _verticalBorded = 20f;
        [SerializeField] private float _leftBorder = 150f;
        [SerializeField] private float _rightBorder = 210f;

        private Camera _camera;
        private float _yCameraRotation = 0f;
        private float _finalXInput;
        private float _finalYInput;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _finalXInput = Mathf.Clamp(_floatingJoystick.Horizontal * _speedXAxis * Time.deltaTime, -MaxInputValue, MaxInputValue);
            _finalYInput = Mathf.Clamp(_floatingJoystick.Vertical * _speedYAxis * Time.deltaTime, -MaxInputValue, MaxInputValue);
            _yCameraRotation -= _finalYInput;
            _yCameraRotation = Mathf.Clamp(_yCameraRotation, -_verticalBorded, _verticalBorded);
            _camera.transform.localRotation = Quaternion.Euler(_yCameraRotation, 0, 0);
            _weapon.transform.localRotation = Quaternion.Euler(-_yCameraRotation, 0, 0);

            if (((transform.localRotation.eulerAngles.y < _rightBorder)
                && (_finalXInput > 0))
                || ((transform.localRotation.eulerAngles.y > _leftBorder)
                && (_finalXInput < 0)))
            {
                transform.Rotate(Vector3.up * _finalXInput);
            }
        }
    }
}