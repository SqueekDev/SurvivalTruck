using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]

    public class ZombieAwayThrower : MonoBehaviour
    {
        [SerializeField] private Vector2 _throwAwayOffsetX;
        [SerializeField] private Vector2 _throwAwayOffsetZ;
        [SerializeField] private float _throwAwaySpeed;

        private Coroutine _throwingAway;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ThrowAway(Transform targetFrom)
        {
            if (_throwingAway == null)
            {
                _throwingAway = StartCoroutine(ThrowingAway(targetFrom));
            }
        }

        public void StopThrowingAway()
        {
            if (_throwingAway != null)
            {
                StopCoroutine(_throwingAway);
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private IEnumerator ThrowingAway(Transform targetFrom)
        {
            float randomX = Random.Range(_throwAwayOffsetX.x, _throwAwayOffsetX.y);
            float randomZ = Random.Range(_throwAwayOffsetZ.x, _throwAwayOffsetZ.y);
            float newPositionX, newPositionY, newPositionZ;

            if (transform.position.x > targetFrom.position.x)
            {
                newPositionX = transform.position.x + randomX;
            }
            else
            {
                newPositionX = transform.position.x - randomX;
            }

            newPositionY = transform.position.y;
            newPositionZ = transform.position.z + randomZ;
            Vector3 newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
            Vector3 direction = newPosition - transform.position;

            while (transform.position.x != newPosition.x)
            {
                _rigidbody.AddForceAtPosition(
                    direction.normalized * _throwAwaySpeed * Time.deltaTime,
                    transform.position,
                    ForceMode.Impulse);
                yield return null;
            }
        }
    }
}