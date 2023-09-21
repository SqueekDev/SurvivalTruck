using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerState : BossState
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_target.transform.position.x - transform.position.x, 0, _target.transform.position.z - transform.position.z).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
