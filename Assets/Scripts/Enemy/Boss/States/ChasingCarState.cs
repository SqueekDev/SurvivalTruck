using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingCarState : BossState
{
    [SerializeField] private float _extraSpeed;
    [SerializeField] private Car _car;

    private float _speed;

    private void Awake()
    {
        _speed = _car.Speed + _extraSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
