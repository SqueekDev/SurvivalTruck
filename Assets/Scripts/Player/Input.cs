using UnityEngine;
using Base;

namespace Player
{
    public class Input : MonoBehaviour
    {
        private const string RunTrigger = "Run";

        [SerializeField] private VariableJoystick _joystick;

        private PlayerMover _mover;
        private Shooter _shooter;
        private Animator _animator;

        private void Start()
        {
            _mover = GetComponent<PlayerMover>();
            _shooter = GetComponent<Shooter>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector3 newDirection = new Vector3(_joystick.Horizontal, transform.position.y, _joystick.Vertical);
            Rotate(newDirection);

            if (_joystick.Horizontal != GlobalValues.Zero && _joystick.Vertical != GlobalValues.Zero)
            {
                Run(newDirection);
                return;
            }

            StopRun();
        }

        private void Run(Vector3 direction)
        {
            _mover.Move(direction);

            if (_animator.GetBool(RunTrigger) == false)
            {
                _animator.SetBool(RunTrigger, true);
            }
        }

        private void StopRun()
        {
            if (_animator.GetBool(RunTrigger))
            {
                _animator.SetBool(RunTrigger, false);
            }
        }

        private void Rotate(Vector3 direction)
        {
            if (_shooter.IsShooting == false)
            {
                if (_joystick.Horizontal != GlobalValues.Zero && _joystick.Vertical != GlobalValues.Zero)
                {
                    _mover.Rotate(new Vector3(transform.position.x + direction.x, transform.position.y,
                        transform.position.z + direction.z));
                }
                return;
            }

            _mover.Rotate(_shooter.Target.transform.position);
        }
    }
}