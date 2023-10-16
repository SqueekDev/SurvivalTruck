using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _shootParticalPrefab;

    private int _damage;
    private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ZombieHealth health))
        {
            health.TakeDamage(_damage);
            //Instantiate(_shootParticalPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void MoveTo(Transform target)
    {
        StartCoroutine(Moving(target));
    }

    private IEnumerator Moving(Transform target)
    {
        while (target.gameObject.activeSelf||transform.position!=target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }
        //Instantiate(_shootParticalPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
