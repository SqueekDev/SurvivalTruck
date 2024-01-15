using Base;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CameraDescendingState : State
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Input _playerInput;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private MoverToShootPlace _moverToShootPlace;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _shooter.enabled = false;
            _playerInput.enabled = false;
            _playerMover.enabled = false;
            _rigidbody.isKinematic = true;
            _moverToShootPlace.enabled = true;
        }
    }
}