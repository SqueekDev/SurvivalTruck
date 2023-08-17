using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMover : Mover
{
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private float _movingDistance;
    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;
    private Coroutine _sideMoving;
    private Coroutine _movingTo;
    private bool _needMoveForward=true;
    private bool _needMoveToTarget =false;
    private Animator _animator;

    private void OnEnable()
    {
        if (_needMoveForward==false)
        {
            _needMoveForward = true;
        }
        if (_needMoveToTarget ==true)
        {
            _needMoveToTarget  = false;
        }
    }

    private void Start()
    {
      _animator=GetComponent<Animator>();

    _camera = Camera.main;
    }

    private void Update()
    {
        if (_needMoveForward)
        {
            MoveForward();
        }
        if (_needMoveToTarget &&_movingTo==null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _movingDistance, _layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Obstacle obstacle))
                {
                    MoveTo(obstacle.transform);
                    
                }
                else if (collider.gameObject.TryGetComponent(out Health health))
                {
                    MoveTo(health.transform);
                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cabine cabine))
        {
            MoveSide();
        }
        if (other.TryGetComponent(out RageArea rageArea))
        {
            _needMoveToTarget  = true;
        }
        if (other.TryGetComponent(out Player player)|| other.TryGetComponent(out Obstacle obstacle))
        {
            StopMoveTo();
        }
    }

    public void MoveForward()
    {
        if (transform.position.z<_camera.transform.position.z-_zLimits.x)
        {
            transform.eulerAngles = new Vector3(0,0,0);

        }
        if (transform.position.z>_camera.transform.position.z+_zLimits.y)
        {
            transform.eulerAngles = new Vector3(0,180,0);
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            Vector3 newPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        transform.Translate(Vector3.forward*_speed*Time.deltaTime);

    }

    public void MoveSide()
    {
        if (_sideMoving==null)
        {
            _sideMoving=StartCoroutine(SideMoving());
        }
    }
    
    public void MoveTo(Transform target)
    {
        if (_movingTo==null)
        {
            _needMoveToTarget  = false;
            _sideMoving=StartCoroutine(MovingTo(target));
        }
    }
    
    public void StopMoveTo()
    {
        if (_movingTo!=null)
        {
            StopCoroutine(_movingTo);
            _movingTo = null;
        }
    }

    private IEnumerator SideMoving()
    {
        _needMoveForward = false;
        int randomAngle = Random.Range(0,2);
        float startSpeed = _speed;
        _speed *= 9;
        Vector3 newPosition;
        if (randomAngle==0)
        {
            newPosition = new Vector3(transform.position.x+15,transform.position.y,transform.position.z);
        }
        else
        {
            newPosition = new Vector3(transform.position.x -15, transform.position.y, transform.position.z);

        }
        _animator.SetTrigger("Jump");
        while (transform.position.x!=newPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_speed*Time.deltaTime);
            yield return null;
        }
        _speed = startSpeed;
        _sideMoving = null;
        _needMoveForward = true;
    }

    private IEnumerator MovingTo(Transform target)
    {
        _needMoveForward = false;
        Vector3 newPosition = new Vector3(target.position.x,transform.position.y,target.position.z);
        while (transform.position!=newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_speed*Time.deltaTime);
            yield return null;
        }
        _movingTo = null;
    }

    public void SetNeedMoveToTarget()
    {
        _needMoveToTarget = true;
    }
}
