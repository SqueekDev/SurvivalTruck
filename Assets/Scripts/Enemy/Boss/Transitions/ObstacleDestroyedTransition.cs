using Base;
using Truck;
using UnityEngine;

namespace Enemy
{
    public class ObstacleDestroyedTransition : Transition
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
}