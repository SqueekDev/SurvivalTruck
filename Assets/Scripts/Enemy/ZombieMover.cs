using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieMover : Mover
{
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Vector2 _xLimits;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpOffset;
    [SerializeField] private float _throwAwaySpeed;
    [SerializeField] private float _backMoveForce;
    [SerializeField] private Vector2 _jumpLimits;
    [SerializeField] private Vector2 _throwAwayOffsetX;
    [SerializeField] private Vector2 _throwAwayOffsetZ;

    private Camera _camera;
    private Coroutine _jumping;
    private Coroutine _throwingAway;
    private Rigidbody _rigidbody;
    private Transform _jumpPoint;

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveForward()
    {
        if (transform.position.z<_camera.transform.position.z-_zLimits.x)
        {
            transform.eulerAngles = new Vector3(0,0,0);
            if (Speed==_startSpeed)
                Speed *= _backMoveForce;
        }
        if (transform.position.z>_camera.transform.position.z+_zLimits.y)
        {
            Speed = _startSpeed;
            transform.eulerAngles = new Vector3(0,180,0);
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            Vector3 newPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        transform.Translate(Vector3.forward*Speed*Time.deltaTime);

    }

    public void ThrowAway(Transform targetFrom)
    {

        if (_throwingAway==null)
        {
            _throwingAway = StartCoroutine(ThrowingAway(targetFrom));

        }
    }

    public void StopThrowingAway()
    {
        if (_throwingAway != null)
        {
            StopCoroutine(_throwingAway);
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private IEnumerator ThrowingAway(Transform targetFrom)
    {
        float randomX = Random.Range(_throwAwayOffsetX.x, _throwAwayOffsetX.y);
        float randomZ = Random.Range(_throwAwayOffsetZ.x, _throwAwayOffsetZ.y);
        float newPositionX, newPositionY, newPositionZ;
        if (transform.position.x > targetFrom.position.x)
        {
            newPositionX = transform.position.x + randomX;
        }
        else
        {
            newPositionX = transform.position.x - randomX;

        }
        newPositionY = transform.position.y;
        newPositionZ = transform.position.z + randomZ;
        Vector3 newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        Vector3 direction=newPosition-transform.position;
        while (true)
        {       
            _rigidbody.AddForceAtPosition(direction.normalized*_throwAwaySpeed, transform.position, ForceMode.Impulse);
            yield return null;
        }
    }
    
    public void MoveTo(Transform target)
    {
        float offset = Random.Range(-1.5f,1.5f);
        Vector3 newDirection = new Vector3(target.position.x+offset, transform.position.y, target.position.z+offset);
        Rotate(newDirection);
        if (target.TryGetComponent(out Player player)&&Vector3.Distance(transform.position,player.transform.position)<5f)
        {
            transform.Translate(Vector3.forward * Speed*0.5f * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

    }


    public void Jump()
    {

        if (_jumping == null)
        {
            _jumping = StartCoroutine(Jumping());
        }
        else
        {
            StopJump();
            _jumping = StartCoroutine(Jumping());
        }
    }

    public void StopJump()
    {
        if (_jumping != null)
        {
            StopCoroutine(_jumping);
        }
    }
    private IEnumerator Jumping()
    {
        float randomZ = Random.Range(_jumpLimits.x,_jumpLimits.y);
        Vector3 newPosition = new Vector3(_jumpPoint.position.x,_jumpPoint.position.y,randomZ+_jumpPoint.position.z);

        while (transform.position!=newPosition)
        {
            newPosition = new Vector3(_jumpPoint.position.x, _jumpPoint.position.y, randomZ + _jumpPoint.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, _jumpForce * Time.deltaTime);
            yield return null;
        }
        transform.SetParent(_jumpPoint.parent.parent);
        _jumping = null;
    }

    public void SetJumpPoint(Transform jumpPoint)
    {
        _jumpPoint = jumpPoint;
    }
}
