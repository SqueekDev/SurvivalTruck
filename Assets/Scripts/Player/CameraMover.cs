using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private const float MaxInputValue = 1f;

    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _speed = 1000f;
    [SerializeField] private float _verticalBorded = 20f;
    [SerializeField] private float _leftBorder = 160f;
    [SerializeField] private float _rightBorder = 200f;

    private Camera _camera;
    private float _yRotation = 0f;
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
        _yRotation -= _yInput;
        _yRotation = Mathf.Clamp(_yRotation, -_verticalBorded, _verticalBorded);
        _camera.transform.localRotation = Quaternion.Euler(_yRotation, 0, 0);
        _weapon.transform.localRotation = _camera.transform.localRotation;

        if (transform.localRotation.eulerAngles.y < _rightBorder && _xInput > 0 || transform.localRotation.eulerAngles.y > _leftBorder && _xInput < 0)
            transform.Rotate(Vector3.up * _xInput);
    }
}
