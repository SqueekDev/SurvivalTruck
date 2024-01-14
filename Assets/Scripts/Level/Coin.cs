using System.Collections;
using UnityEngine;

namespace Level
{
    public class Coin : MonoBehaviour
    {
        private const float MoveDelayTime = 0.2f;

        [SerializeField] private float _speed;

        private Coroutine _moving;
        private WaitForSeconds _moveDelay = new WaitForSeconds(MoveDelayTime);

        private IEnumerator Moving(Transform target)
        {
            yield return _moveDelay;

            while (Vector3.Distance(transform.position, target.position) > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }

            gameObject.SetActive(false);
        }
        public void MoveTarget(Transform target)
        {
            _moving = StartCoroutine(Moving(target));
        }
    }
}