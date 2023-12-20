using UnityEngine;

public class ObstacleDestroyedTransition : BossTransition
{
    [SerializeField] private Obstacle _obstacle;

    private void Update()
    {
        if (_obstacle.IsDestroyed)
        {
            NeedTransit = true;
        }
    }
}
