using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMover : Mover
{
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Player _player;

    private Camera _camera;
    private Coroutine _sideMoving;
    private Coroutine _movingTo;
    private bool _needMoveForward = true;
    private bool _needJump = true;
    private Animator _animator;

    private void OnEnable()
    {
        if (_needMoveForward == false)
        {
            _needMoveForward = true;
        }
        if (_needJump == false)
        {
            _needJump = true;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_needMoveForward)
        {
            MoveForward();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cabine cabine))
        {
            MoveSide(cabine.transform);
        }
        if (other.TryGetComponent(out RageArea rageArea))
        {
            MoveTo(rageArea.Obstacle.transform);
        }
    }

    public void MoveForward()
    {
        if (transform.position.z < _camera.transform.position.z - _zLimits.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        if (transform.position.z > _camera.transform.position.z + _zLimits.y)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            Vector3 newPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

    }

    public void MoveSide(Transform target)
    {
        if (_sideMoving == null)
        {
            _sideMoving = StartCoroutine(SideMoving(target));
        }
    }

    public void MoveTo(Transform target)
    {
        if (_movingTo != null)
        {
            StopMoveTo();
        }
        if (_needMoveForward)
        {
            _needMoveForward = false;
        }
        _movingTo = StartCoroutine(MovingTo(target));
    }

    public void StopMoveTo()
    {
        if (_movingTo!=null)
        {
            StopCoroutine(_movingTo);
            _movingTo = null;
        }      
    }

    private IEnumerator SideMoving(Transform target)
    {
        _needMoveForward = false;
        int randomAngle = Random.Range(0, 2);
        float startSpeed = Speed;
        Speed *= 9;
        Vector3 newPosition;
        if (randomAngle == 0)
        {
            newPosition = new Vector3(target.position.x + 10, transform.position.y, transform.position.z);
        }
        else
        {
            newPosition = new Vector3(target.position.x - 10, transform.position.y, transform.position.z);

        }
        _animator.SetTrigger("Jump");
        while (transform.position.x != newPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Speed * Time.deltaTime);
            yield return null;
        }
        Speed = startSpeed;
        _sideMoving = null;
        _needMoveForward = true;
    }

    private IEnumerator MovingTo(Transform target)
    {
        transform.SetParent(target.parent);
        Vector3 newPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        while (true)
        {
            newPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, Speed * Time.deltaTime);
            transform.LookAt(newPosition);
            yield return null;
        }
    }

    public void Jump()
    {
        if (_needJump)
        {
            _needJump = false;
            float newPositionY = transform.position.y + _jumpForce;
            _animator.SetTrigger("Jump");
            MoveTo(_player.transform);
            transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
        }
    }
}
