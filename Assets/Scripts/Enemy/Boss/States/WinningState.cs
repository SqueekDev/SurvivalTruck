using Base;
using Truck;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class WinningState : State
    {
        private const string WinAnimationName = "Win";

        [SerializeField] private Car _car;

        private Vector3 _offset;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _offset = _car.transform.position - transform.position;
            _animator.SetTrigger(WinAnimationName);
        }

        private void FixedUpdate()
        {
            transform.position = _car.transform.position - _offset;
        }
    }
}