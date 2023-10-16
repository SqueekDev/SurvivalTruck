using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RageArea : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private Player _player;

    public Obstacle Obstacle => _obstacle;

    public event UnityAction<ZombieHealth> ZombieAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ZombieHealth zombieHealth))
            ZombieAttacked?.Invoke(zombieHealth);
    }

    public Transform GetTarget()
    {
        if (_obstacle.IsDestroyed)
        {
            return _player.transform;
        }
        else
        {
            return _obstacle.transform;
        }
    }
}
