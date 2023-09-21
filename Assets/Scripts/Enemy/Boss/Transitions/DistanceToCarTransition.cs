using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToCarTransition : BossTransition
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (_target.position.z - transform.position.z < _transitionRange)
            NeedTransit = true;
    }
}
