using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDistanceTransition : BossTransition
{
    [SerializeField] private PushBackState _pushBackState;

    private void OnEnable()
    {
        NeedTransit = false;
        _pushBackState.Fell += OnFell;
    }

    private void OnDisable()
    {
        _pushBackState.Fell -= OnFell;        
    }

    private void OnFell()
    {
        NeedTransit = true;
    }
}
