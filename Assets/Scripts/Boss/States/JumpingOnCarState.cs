using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingOnCarState : BossState
{
    [SerializeField] private WoodBlock _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _zOffset;

    protected WoodBlock Target => _target;
    protected float ZOffset => _zOffset;

    private void FixedUpdate()
    {
        Vector3 target = new Vector3(_target.transform.position.x, _target.transform.position.y + _yOffset, _target.transform.position.z - _zOffset);
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
