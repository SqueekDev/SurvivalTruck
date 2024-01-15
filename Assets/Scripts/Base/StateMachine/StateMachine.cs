using UnityEngine;

namespace Base
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State _firstState;
        [SerializeField] private DyingState _dyingState;
        [SerializeField] private Health _health;

        private State _currentState;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Died += OnDying;
            Transit(_firstState);
        }

        private void OnDisable()
        {
            _health.Died -= OnDying;
        }

        private void Update()
        {
            if (_currentState == null)
            {
                return;
            }

            var nextState = _currentState.GetNextState();

            if (nextState != null)
            {
                Transit(nextState);
            }
        }

        private void Transit(State nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = nextState;

            if (_currentState != null)
            {
                _currentState.Enter();
            }
        }

        private void OnDying(Health health)
        {
            Transit(_dyingState);
        }
    }
}
