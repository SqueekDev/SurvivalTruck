using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackTransition : BossTransition
{
    [SerializeField] private Health _head;

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Update()
    {
        if (_head.IsDead)
            NeedTransit = true;
    }
}
