using UnityEngine;
using Base;

namespace Enemy
{
    [RequireComponent(typeof(Boss))]
    public class BossStateMachine : MonoBehaviour
    {
        [SerializeField] private BossState _firstState;
        [SerializeField] private DyingState _dyingState;

        private Boss _boss;
        private BossState _currentState;

        private void Awake()
        {
            _boss = GetComponent<Boss>();
        }

        private void OnEnable()
        {
            _boss.Died += OnDying;
            Transit(_firstState);
        }

        private void OnDisable()
        {
            _boss.Died -= OnDying;
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

        private void Transit(BossState nextState)
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

        private void OnDying(Health boss)
        {
            Transit(_dyingState);
        }
    }
}
