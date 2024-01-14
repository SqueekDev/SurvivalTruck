using System.Collections;
using Base;
using Level;
using Truck;
using UnityEngine;

namespace Enemy
{
    public class ChasingCarState : BossState
    {
        private const int Correction = 1;
        private const float MultiplierDivider = 20;
        private const float TimeToChangeDirection = 1;

        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private float _extraSpeed;
        [SerializeField] private Car _car;

        private Coroutine _changeDirectionCorutine;
        private float _speed;
        private float _startXPosition;
        private float _xPositionLimit = 2f;
        private float _startXDirectionSpread = 0.5f;
        private float _additionalXDirectionSpread;
        private float _zDirection = 1f;
        private Vector3 _direction;
        private WaitForSeconds _delayToChangeDirection = new WaitForSeconds(TimeToChangeDirection);

        private void Awake()
        {
            _startXPosition = transform.position.x;
            _speed = _car.Speed + _extraSpeed;
        }

        private void OnEnable()
        {
            if (_changeDirectionCorutine != null)
            {
                StopCoroutine(_changeDirectionCorutine);
            }

            ChangeRunningSpread();
            _changeDirectionCorutine = StartCoroutine(ChangeDirection());
        }

        private void FixedUpdate()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

        private IEnumerator ChangeDirection()
        {
            float xDirectionSpread = _startXDirectionSpread + _additionalXDirectionSpread;

            while (enabled)
            {
                float xDirection = Random.Range(-xDirectionSpread, xDirectionSpread);

                if (transform.position.x > _startXPosition + _xPositionLimit &&
                    xDirection > GlobalValues.Zero ||
                    transform.position.x < _startXPosition - _xPositionLimit &&
                    xDirection < GlobalValues.Zero)
                {
                    xDirection = -xDirection;
                }

                _direction = new Vector3(xDirection, GlobalValues.Zero, _zDirection);
                yield return _delayToChangeDirection;
            }
        }

        private void ChangeRunningSpread()
        {
            int currentSpreadMultiplier = _levelChanger.CurrentLevelNumber / (_levelChanger.BossLevelNumber + Correction);
            _additionalXDirectionSpread = currentSpreadMultiplier / MultiplierDivider;
        }
    }
}
