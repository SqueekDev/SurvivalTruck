using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(direction.x * _speed,
            _rigidbody.velocity.y, direction.z * _speed);
    }
}
