using System.Collections;
using UnityEngine;

public class BossBullet : Bullet
{
    protected override IEnumerator Moving(Transform target)
    {
        Vector3 startTargetPositon = target.position;

        while (target.gameObject.activeSelf || transform.position != startTargetPositon)
        {
            transform.position = Vector3.MoveTowards(transform.position, startTargetPositon, Speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
