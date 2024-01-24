using System.Collections;
using Base;
using UnityEngine;

namespace Level
{
    public class Coin : PoolObject
    {
        private const float MoveDelayTime = 0.2f;
        private const float CollectDistance = 1f;

        [SerializeField] private float _speed;

        private Coroutine _moving;
        private WaitForSeconds _moveDelay = new WaitForSeconds(MoveDelayTime);

        public void MoveTarget(Transform target)
        {
            if (_moving != null)
            {
                StopCoroutine(Moving(target));
            }

            _moving = StartCoroutine(Moving(target));
        }

        private IEnumerator Moving(Transform target)
        {
            yield return _moveDelay;

            while (Vector3.Distance(transform.position, target.position) > CollectDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}