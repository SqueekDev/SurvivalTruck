using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class ZombieJumper : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private Vector2 _jumpLimits;

        private Coroutine _jumping;
        private Transform _jumpPoint;

        public void Jump()
        {
            if (_jumping == null)
            {
                _jumping = StartCoroutine(Jumping());
            }
            else
            {
                StopJump();
                _jumping = StartCoroutine(Jumping());
            }
        }

        public void StopJump()
        {
            if (_jumping != null)
            {
                StopCoroutine(_jumping);
                transform.SetParent(_jumpPoint.parent.parent);
                _jumping = null;
            }
        }

        public void SetJumpPoint(Transform jumpPoint)
        {
            _jumpPoint = jumpPoint;
        }

        private IEnumerator Jumping()
        {
            float randomZ = Random.Range(_jumpLimits.x, _jumpLimits.y);
            Vector3 newPosition = new Vector3(_jumpPoint.position.x, _jumpPoint.position.y, randomZ + _jumpPoint.position.z);

            while (transform.position.x != newPosition.x || transform.position.z != newPosition.z)
            {
                newPosition = new Vector3(_jumpPoint.position.x, _jumpPoint.position.y, randomZ + _jumpPoint.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, _jumpForce * Time.deltaTime);
                yield return null;
            }

            _jumping = null;
        }
    }
}