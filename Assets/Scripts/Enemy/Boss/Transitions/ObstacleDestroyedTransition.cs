using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyedTransition : BossTransition
{
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private Health _head;

    private void Update()
    {
        if (_obstacle.IsDestroyed)
        {
            _head.gameObject.SetActive(false);
            NeedTransit = true;
        }
    }
}
