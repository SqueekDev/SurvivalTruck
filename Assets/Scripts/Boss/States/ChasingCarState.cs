using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingCarState : BossState
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
