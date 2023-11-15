using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator Moving(Transform target)
    {
        while (target.gameObject.activeSelf || transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void MoveTo(Transform target)
    {
        StartCoroutine(Moving(target));
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
