using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverToShootPlace : MonoBehaviour
{
    [SerializeField] private ShootPlace _shootPlace;
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _rotationSpeed = 10f;

    private float _targetRotation = 180f;
    private float _rotateInaccuracy = 1f;
    private bool _achievedTarget;
    private bool _rotatedToTarget;

    private void OnEnable()
    {
        _shootPlace.gameObject.SetActive(true);
        _rotatedToTarget = false;
        _achievedTarget = false;
    }

    private void Update()
    {
        if (_achievedTarget && _rotatedToTarget)
            enabled = false;
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _shootPlace.transform.localPosition, _speed * Time.deltaTime);
        RotateToTarget();
    }

    private void OnDisable()
    {
        _shootPlace.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ShootPlace shootPlace))
            _achievedTarget = true;
    }

    private void RotateToTarget()
    {
        if (transform.eulerAngles.y >= _targetRotation - _rotateInaccuracy && transform.eulerAngles.y <= _targetRotation + _rotateInaccuracy)
        {
            transform.localRotation = Quaternion.Euler(0, _targetRotation, 0);
            _rotatedToTarget = true;
        }
        else
        {
            Quaternion direcrion = Quaternion.Euler(0, _targetRotation, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, direcrion, _rotationSpeed * Time.deltaTime);
        }
    }
}
