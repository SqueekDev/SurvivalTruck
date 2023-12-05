using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RageArea : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacle;

    public Obstacle Obstacle => _obstacle;

    public event UnityAction<ZombieHealth> ZombieAttacked;

    public void Attacked(ZombieHealth zombie)
    {
        ZombieAttacked?.Invoke(zombie);
        Debug.Log("tr");
    }
}
