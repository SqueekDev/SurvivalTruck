using Base;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CameraClimbingState : State
    {
        [SerializeField] private BossShooter _bossShooter;
        [SerializeField] private CameraMover _cameraMover;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _bossShooter.enabled = false;
            _cameraMover.enabled = false;
            _rigidbody.isKinematic = false;
        }
    }
}
