using Base;
using UnityEngine;

namespace Player
{
    public class BossShootingState : State
    {
        [SerializeField] private MoverToShootPlace _moverToShootPlace;
        [SerializeField] private BossShooter _bossShooter;
        [SerializeField] private CameraMover _cameraMover;

        private void OnEnable()
        {
            _moverToShootPlace.enabled = false;
            _bossShooter.enabled = true;
            _cameraMover.enabled = true;
        }
    }
}