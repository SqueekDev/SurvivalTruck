using Base;
using UnityEngine;

namespace Player
{
    public class PlayerDyingState : DyingState
    {
        [SerializeField] private MoverToShootPlace _moverToShootPlace;
        [SerializeField] private BossShooter _bossShooter;
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Input _playerInput;
        [SerializeField] private PlayerMover _playerMover;

        private void OnEnable()
        {
            _shooter.enabled = false;
            _playerMover.enabled = false;
            _playerInput.enabled = false;
            _cameraMover.enabled = false;
            _bossShooter.enabled = false;
            _moverToShootPlace.enabled = false;
        }
    }
}