using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RageArea : MonoBehaviour
{
    public event UnityAction<Health> ZombieAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health zombie))
        {
            zombie.Died += OnZOmbieDied;
            ZombieAttacked?.Invoke(zombie);
        }
    }

    private void OnZOmbieDied(Health zombie)
    {
        zombie.Died -= OnZOmbieDied;
    }
}
