using Player;
using UnityEngine;

namespace CameraController
{
    public class CameraUpperPoint : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _player;

        private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - _player.transform.position;
        }

        private void FixedUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}