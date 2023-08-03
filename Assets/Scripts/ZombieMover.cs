using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMover : Mover
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _zLimits;

    private Camera _camera;
    private Coroutine _sideMoving;
    private bool _needMoveForward=true;

    private void Start()
    {
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
            MoveSide();
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

    private IEnumerator SideMoving()
    {
        _needMoveForward = false;
        int randomAngle = Random.Range(0,2);
        float startSpeed = _speed;
        _speed *= 10;
        Vector3 newPosition;
        if (randomAngle==0)
        {
            newPosition = new Vector3(transform.position.x+15,transform.position.y,transform.position.z);
        }
        else
        {
            newPosition = new Vector3(transform.position.x -15, transform.position.y, transform.position.z);

        }
        while (transform.position.x!=newPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position,newPosition,_speed*Time.deltaTime);
            yield return null;
        }
        _speed = startSpeed;
        _sideMoving = null;
        _needMoveForward = true;
    }
}
