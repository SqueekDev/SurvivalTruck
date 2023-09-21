using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BossStateMachine : MonoBehaviour
{
    [SerializeField] private BossState _firstState;
    [SerializeField] private DyingState _dyingState;

    private Health _boss;
    private BossState _currentState;

    public BossState CurrentState => _currentState;

    private void Awake()
    {
        _boss = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _boss.Died += OnDying;
    }

    private void OnDisable()
    {
        _boss.Died -= OnDying;
    }

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(BossState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void Transit(BossState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void OnDying(Health boss)
    {
        Transit(_dyingState);
    }
}
