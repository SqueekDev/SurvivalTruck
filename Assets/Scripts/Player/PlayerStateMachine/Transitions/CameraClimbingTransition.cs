using Base;
using CameraController;
using UnityEngine;

namespace Player
{
    public class CameraClimbingTransition : Transition
    {
        [SerializeField] private CameraPositionChanger _cameraPositionChanger;

        private void Awake()
        {
            _cameraPositionChanger.Climbed += OnCameraClimbed;
        }

        private void OnDestroy()
        {
            _cameraPositionChanger.Climbed -= OnCameraClimbed;
        }

        private void OnCameraClimbed()
        {
            NeedTransit = true;
        }
    }
}