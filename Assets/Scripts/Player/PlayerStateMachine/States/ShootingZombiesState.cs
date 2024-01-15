using Base;
using UnityEngine;

namespace Player
{
    public class ShootingZombiesState : State
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Input _playerInput;
        [SerializeField] private PlayerMover _playerMover;

        private void OnEnable()
        {
            _shooter.enabled = true;
            _playerInput.enabled = true;
            _playerMover.enabled = true;
        }
    }
}