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
    private Vector3 _input;
    private float _finalXInput;
    private float _finalYInput;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _input = new Vector3(_floatingJoystick.Horizontal, _floatingJoystick.Vertical).normalized;
    }

    private void FixedUpdate()
    {
        _finalXInput = Mathf.Clamp(_input.x * _speed * Time.deltaTime, -MaxInputValue, MaxInputValue);
        _finalYInput = Mathf.Clamp(_input.y * _speed * Time.deltaTime, -MaxInputValue, MaxInputValue);
        _yCameraRotation -= _finalYInput;
        _yCameraRotation = Mathf.Clamp(_yCameraRotation, -_verticalBorded, _verticalBorded);
        _camera.transform.localRotation = Quaternion.Euler(_yCameraRotation, GlobalValues.Zero, GlobalValues.Zero);
        _weapon.transform.localRotation = Quaternion.Euler(-_yCameraRotation, GlobalValues.Zero, GlobalValues.Zero);

        if (transform.localRotation.eulerAngles.y < _rightBorder && _finalXInput > GlobalValues.Zero || transform.localRotation.eulerAngles.y > _leftBorder && _finalXInput < GlobalValues.Zero)
        {
            transform.Rotate(Vector3.up * _finalXInput);
        }
    }
}
