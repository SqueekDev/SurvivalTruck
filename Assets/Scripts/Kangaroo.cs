using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangaroo : Cabine
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            StartCoroutine(ApplyingDamage(health));
        }
    }

    private IEnumerator ApplyingDamage(Health health)
    {
        yield return new WaitForSeconds(0.5f);
        health.TakeDamage(_damage);
    }
}
