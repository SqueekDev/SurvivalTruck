using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _speed = 1000f;
    [SerializeField] private float _verticalBorded = 20f;
    [SerializeField] private float _leftBorder = 150f;
    [SerializeField] private float _rightBorder = 150f;

    private float _xRotation = 0f;
    private float _xInput;
    private float _yInput;

    private void Update()
    {
        _xInput = _floatingJoystick.Horizontal * _speed * Time.deltaTime;
        _yInput = _floatingJoystick.Vertical * _speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _xRotation -= _yInput;
        _xRotation = Mathf.Clamp(_xRotation, -_verticalBorded, _verticalBorded);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _weapon.transform.localRotation = transform.localRotation;

        if (_player.transform.localRotation.eulerAngles.y < _rightBorder && _xInput > 0 || _player.transform.localRotation.eulerAngles.y > _leftBorder && _xInput < 0)
            _player.transform.Rotate(Vector3.up * _xInput);
    }
}
