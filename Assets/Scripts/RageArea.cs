using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RageArea : MonoBehaviour
{
    public event UnityAction<Zombie> ZombieAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Zombie zombie))
        {
            zombie.Died += OnZOmbieDied;
            ZombieAttacked?.Invoke(zombie);
        }
    }

    private void OnZOmbieDied(Zombie zombie)
    {
        zombie.Died -= OnZOmbieDied;
    }
}
