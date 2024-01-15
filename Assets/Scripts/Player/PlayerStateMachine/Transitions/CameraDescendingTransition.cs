using Base;
using CameraController;
using UnityEngine;

namespace Player
{
    public class CameraDescendingTransition : Transition
    {
        [SerializeField] private CameraPositionChanger _cameraPositionChanger;

        private void Awake()
        {
            _cameraPositionChanger.Descended += OnCameraDescended;
        }

        private void OnDestroy()
        {
            _cameraPositionChanger.Descended -= OnCameraDescended;
        }

        private void OnCameraDescended()
        {
            NeedTransit = true;
        }
    }
}