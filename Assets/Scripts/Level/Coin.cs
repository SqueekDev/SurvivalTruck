using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDelay=0.2f;

    private Coroutine _moving;

    private IEnumerator Moving(Transform target)
    {
        yield return new WaitForSeconds(_moveDelay);

        while (Vector3.Distance(transform.position,target.position)>1)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.position,_speed*Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    public void MoveTarget(Transform target)
    {
        _moving = StartCoroutine(Moving(target));
    }
}
