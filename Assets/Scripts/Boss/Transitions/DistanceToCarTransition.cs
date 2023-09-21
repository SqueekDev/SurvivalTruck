using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToCarTransition : BossTransition
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < _transitionRange)
            NeedTransit = true;
    }
}
