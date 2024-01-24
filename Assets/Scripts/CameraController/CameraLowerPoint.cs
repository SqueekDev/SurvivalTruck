using Base;
using Truck;
using UnityEngine;

namespace CameraController
{
    public class CameraLowerPoint : MonoBehaviour
    {
        [SerializeField] private Health _player;
        [SerializeField] private Car _car;

        private void OnEnable()
        {
            _player.Died += OnPlayerDied;
        }

        private void OnDisable()
        {
            _player.Died -= OnPlayerDied;
        }

        private void OnPlayerDied(Health player)
        {
            Vector3 cameraLowerPointRotation = transform.rotation.eulerAngles;
            transform.parent = _car.transform;
            transform.rotation = Quaternion.Euler(cameraLowerPointRotation);
        }
    }
}