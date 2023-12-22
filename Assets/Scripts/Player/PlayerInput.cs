using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Run = "Run";

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

    private void FixedUpdate()
    {
        Vector3 newDirection = Vector3.zero;
        newDirection = new Vector3(_joystick.Horizontal,0,_joystick.Vertical);

        if (_joystick.Horizontal != GlobalValues.Zero && _joystick.Vertical != GlobalValues.Zero)
        {
            _mover.Move(newDirection);

            if (_animator.GetBool(Run) == false)
            {
                _animator.SetBool(Run, true);
            }
        }
        else
        {
            if (_animator.GetBool(Run))
            {
                _animator.SetBool(Run, false);
            }
        }

        if (_shooter.IsShooting == false)
        {
            if (_joystick.Horizontal != GlobalValues.Zero && _joystick.Vertical != GlobalValues.Zero)
            {
                _mover.Rotate(new Vector3(transform.position.x + newDirection.x, transform.position.y,
                    transform.position.z + newDirection.z));
            }
        }
        else
        {
            _mover.Rotate(_shooter.Target.transform.position);
        }
    }
}
