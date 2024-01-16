using Base;
using Truck;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Input))]

    public class PlayerMover : Mover
    {
        private const string RunTrigger = "Run";

        [SerializeField] private Vector2 _xLimits;
        [SerializeField] private Vector2 _zLimits;
        [SerializeField] private Body _body;
        [SerializeField] private Input _input;
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            _input.JoystickPushed += OnJoystickPushed;
            _input.JoystickPulled += OnJoystickPulled;
        }

        private void OnDisable()
        {
            _input.JoystickPushed -= OnJoystickPushed;
            _input.JoystickPulled -= OnJoystickPulled;

        }

        private void OnJoystickPushed(Vector3 direction)
        {
            Move(direction);

            if (_animator.GetBool(RunTrigger) == false)
            {
                _animator.SetBool(RunTrigger, true);
            }
        }

        private void OnJoystickPulled()
        {
            if (_animator.GetBool(RunTrigger))
            {
                _animator.SetBool(RunTrigger, false);
            }
        }

        private void Move(Vector3 direction)
        {
            Vector3 target = new Vector3(transform.position.x + direction.x * Time.deltaTime * Speed,
                    transform.position.y, transform.position.z + direction.z * Time.deltaTime * Speed);

            if (target.x > _xLimits.y && target.x < _xLimits.x && target.z > _body.transform.position.z + _zLimits.y
                && target.z < _body.transform.position.z + _zLimits.x)
            {
                transform.position = target;
            }
        }
    }
}