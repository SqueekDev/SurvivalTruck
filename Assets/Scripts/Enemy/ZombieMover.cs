using UnityEngine;

public class ZombieMover : Mover
{
    [SerializeField] private float _toPlayerSpeed;
    [SerializeField] private float _toPlayerSlowingDistance;
    [SerializeField] private float _backMoveForce;
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private Vector2 _movingOffset;

    private Camera _camera;
    private Vector3 _startRotationAngle;
    private float _startYRotation = 180f;
    private float _startXRotation = 0f;
    private float _startZRotation = 0f;

    private void Start()
    {
        _startRotationAngle = new Vector3(_startXRotation, _startYRotation, _startZRotation);
        transform.eulerAngles = _startRotationAngle;
        _camera = Camera.main;
    }

    public void MoveForward()
    {
        if (transform.position.z < _camera.transform.position.z - _zLimits.x)
        {
            transform.eulerAngles = Vector3.zero;

            if (Speed == _startSpeed)
            {
                Speed *= _backMoveForce;
            }
        }

        if (transform.position.z > _camera.transform.position.z + _zLimits.y)
        {
            Speed = _startSpeed;
            transform.eulerAngles = _startRotationAngle;
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            Vector3 newPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void MoveTo(Transform target)
    {
        float offset = Random.Range(_movingOffset.x, _movingOffset.y);
        Vector3 newDirection = new Vector3(target.position.x + offset, transform.position.y, target.position.z + offset);
        Rotate(newDirection);

        if (target.TryGetComponent(out PlayerHealth player) && Vector3.Distance(transform.position, player.transform.position)
            < _toPlayerSlowingDistance)
        {
            transform.Translate(Vector3.forward * _toPlayerSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}
