using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingCarState : BossState
{
    [SerializeField] private float _extraSpeed;
    [SerializeField] private Car _car;

    private Coroutine _changeDirectionCorutine;
    private float _speed;
    private float _startXPosition;
    private float _xPositionLimit = 2f;
    private float _xDirectionSpread = 0.5f;
    private float _zDirection = 1f;
    private Vector3 _direction;

    private void Awake()
    {
        _startXPosition = transform.position.x;
        _speed = _car.Speed + _extraSpeed;
    }

    private void OnEnable()
    {
        if (_changeDirectionCorutine != null)
            StopCoroutine(_changeDirectionCorutine);

        _changeDirectionCorutine = StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private IEnumerator ChangeDirection()
    {
        float delayTime = 1f;
        WaitForSeconds delayToChangeDirection = new WaitForSeconds(delayTime);

        while (enabled)
        {
            float xDirection = Random.Range(-_xDirectionSpread, _xDirectionSpread);

            if (transform.position.x > _startXPosition + _xPositionLimit && xDirection > 0 || transform.position.x < _startXPosition - _xPositionLimit && xDirection < 0)
                xDirection = -xDirection;

            _direction = new Vector3(xDirection, 0, _zDirection);
            yield return delayToChangeDirection;
        }
    }
}
