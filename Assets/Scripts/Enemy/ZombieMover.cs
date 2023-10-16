using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class ZombieMover : Mover
{
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Vector2 _xLimits;
<<<<<<< Updated upstream
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
            MoveSide(cabine.transform);
        }
        if (other.TryGetComponent(out RageArea rageArea))
        {
            _needMoveToTarget  = true;
        }
        if (other.TryGetComponent(out Player player)|| other.TryGetComponent(out Obstacle obstacle))
        {
            StopMoveTo();
        }
=======
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpOffset;
    [SerializeField] private float _throwAwaySpeed;

    private Camera _camera;
    private Coroutine _jumping;
    private Coroutine _throwingAway;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
>>>>>>> Stashed changes
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
        transform.Translate(Vector3.forward*Speed*Time.deltaTime);

    }

    public void ThrowAway(Transform targetFrom)
    {
<<<<<<< Updated upstream
        if (_sideMoving==null)
        {
            _sideMoving=StartCoroutine(SideMoving(target));
=======
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
        float randomX = Random.Range(5, 10);
        float randomZ = Random.Range(7, 10);
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
>>>>>>> Stashed changes
        }
    }
    
    public void MoveTo(Transform target)
    {
<<<<<<< Updated upstream
        if (_movingTo==null)
        {
            _needMoveToTarget  = false;
            _movingTo=StartCoroutine(MovingTo(target));
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

    private IEnumerator SideMoving(Transform target)
    {
        _needMoveForward = false;
        int randomAngle = Random.Range(0,2);
        float startSpeed = Speed;
        Speed *= 9;
        Vector3 newPosition;
        if (randomAngle==0)
        {
            newPosition = new Vector3(target.position.x+10,transform.position.y,transform.position.z);
        }
        else
        {
            newPosition = new Vector3(target.position.x -10, transform.position.y, transform.position.z);

        }
        _animator.SetTrigger("Jump");
        while (transform.position.x!=newPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,Speed*Time.deltaTime);
            yield return null;
        }
        Speed = startSpeed;
        _sideMoving = null;
        _needMoveForward = true;
        Debug.Log("jumped");
=======
        float offset = Random.Range(-1.5f,1.5f);
        Vector3 newDirection = new Vector3(target.position.x+offset, transform.position.y, target.position.z+offset);
        Rotate(newDirection);
        if (target.TryGetComponent(out Player player))
        {
            transform.Translate(Vector3.forward * Speed*0.5f * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

>>>>>>> Stashed changes
    }


    public void Jump(Transform target)
    {
<<<<<<< Updated upstream
        _needMoveForward = false;
        Vector3 newPosition = new Vector3(target.position.x,transform.position.y,target.position.z);
        while (transform.position!=newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,Speed *Time.deltaTime);
            yield return null;
        }
        _movingTo = null;
    }

    public void SetNeedMoveToTarget()
    {
        _needMoveToTarget = true;
=======
        if (_jumping == null)
        {
            transform.SetParent(target.parent);
            _jumping = StartCoroutine(Jumping(target));
        }
    }

    public void StopJump()
    {
        if (_jumping != null)
        {
            StopCoroutine(_jumping);
        }
    }
    private IEnumerator Jumping(Transform target)
    {
        float newPositionY =_jumpHeight;
        float newPositionX=0;
        float newPositionZ;
        if (target.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            if (transform.position.x > target.position.x)
                newPositionX = target.position.x + _jumpOffset;
            else if (transform.position.x < target.position.x)
                newPositionX = target.position.x - _jumpOffset;

        }
        if (target.gameObject.TryGetComponent(out Player player))
        {
            if (transform.position.x > target.position.x)
                newPositionX = transform.position.x - _jumpOffset*10;
            else if (transform.position.x < target.position.x)
                newPositionX = transform.position.x + _jumpOffset*10;

        }
        newPositionZ = transform.position.z;       
        Vector3 newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        
        while (true)
        {

            transform.position = Vector3.MoveTowards(transform.position, newPosition, _jumpForce * Time.deltaTime);
            yield return null;
        }

>>>>>>> Stashed changes
    }
}
