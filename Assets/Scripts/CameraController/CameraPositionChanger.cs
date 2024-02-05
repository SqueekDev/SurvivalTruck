using System;
using System.Collections;
using Level;
using UnityEngine;

namespace CameraController
{
    public class CameraPositionChanger : MonoBehaviour
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private CameraUpperPoint _upperPoint;
        [SerializeField] private CameraLowerPoint _lowerPoint;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private Coroutine _moveCoroutine;

        public event Action Descended;

        public event Action Climbed;

        private void OnEnable()
        {
            _levelChanger.BossLevelStarted += OnBossLevelStarted;
            _levelChanger.BossLevelEnded += OnBossLevelEnded;
        }

        private void OnDisable()
        {
            _levelChanger.BossLevelStarted -= OnBossLevelStarted;
            _levelChanger.BossLevelEnded -= OnBossLevelEnded;
        }

        private IEnumerator MoveToTarget(Action action)
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            Quaternion newAngle = Quaternion.Euler(0, 0, 0);

            while (transform.localPosition != newPosition)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, newAngle, _rotateSpeed * Time.deltaTime);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPosition, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.localPosition = newPosition;
            transform.localRotation = newAngle;
            action?.Invoke();
        }

        private void CheckCorutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        private void OnBossLevelStarted()
        {
            transform.parent = _lowerPoint.transform;
            CheckCorutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveToTarget(Descended));
        }

        private void OnBossLevelEnded()
        {
            transform.parent = _upperPoint.transform;
            CheckCorutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveToTarget(Climbed));
        }
    }
}