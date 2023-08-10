using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackTransition : BossTransition
{
    [SerializeField] private Health _head;

    private void OnEnable()
    {
        NeedTransit = false;
        _head.Died += OnKnocked;
    }

    private void OnDisable()
    {
        _head.Died -= OnKnocked;
    }

    private void OnKnocked(Health head)
    {
        NeedTransit = true;
    }
}
