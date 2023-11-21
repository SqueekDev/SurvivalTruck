using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Coroutine _moving;

    public void MoveTarget(Transform target)
    {
       _moving=StartCoroutine(Moving(target));
    }

    private IEnumerator Moving(Transform target)
    {
        yield return new WaitForSeconds(0.2f);
        while (Vector3.Distance(transform.position,target.position)>1)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.position,_speed*Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
