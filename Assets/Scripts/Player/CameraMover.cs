using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private const float MaxInputValue = 1f;

    [SerializeField] private VariableJoystick _floatingJoystick;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _speed = 600f;
    [SerializeField] private float _verticalBorded = 20f;
    [SerializeField] private float _leftBorder = 150f;
    [SerializeField] private float _rightBorder = 210f;

    private Camera _camera;
    private float _yCameraRotation = 0f;
    private float _xInput;
    private float _yInput;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _xInput = Mathf.Clamp(_floatingJoystick.Horizontal * _speed * Time.deltaTime, -MaxInputValue, MaxInputValue);
        _yInput = Mathf.Clamp(_floatingJoystick.Vertical * _speed * Time.deltaTime, -MaxInputValue, MaxInputValue);
    }

    private void FixedUpdate()
    {
        _yCameraRotation -= _yInput;
        _yCameraRotation = Mathf.Clamp(_yCameraRotation, -_verticalBorded, _verticalBorded);
        _camera.transform.localRotation = Quaternion.Euler(_yCameraRotation, GlobalValues.Zero, GlobalValues.Zero);
        _weapon.transform.localRotation = Quaternion.Euler(-_yCameraRotation, GlobalValues.Zero, GlobalValues.Zero);

        if (transform.localRotation.eulerAngles.y < _rightBorder && _xInput > GlobalValues.Zero || transform.localRotation.eulerAngles.y > _leftBorder && _xInput < GlobalValues.Zero)
        {
            transform.Rotate(Vector3.up * _xInput);
        }
    }
}
