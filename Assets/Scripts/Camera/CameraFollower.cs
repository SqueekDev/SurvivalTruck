using UnityEngine;

public class CameraFollower : MonoBehaviour
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
