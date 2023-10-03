using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RageArea : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacle;

    public Obstacle Obstacle => _obstacle;

    public event UnityAction<Zombie> ZombieAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Zombie zombieHealth))
            ZombieAttacked?.Invoke(zombieHealth);
    }
}