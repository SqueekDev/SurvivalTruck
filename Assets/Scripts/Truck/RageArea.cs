using System;
using Enemy;
using UnityEngine;

namespace Truck
{
    public class RageArea : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstacle;

        public event Action<ZombieHealth> ZombieAttacked;

        public Obstacle Obstacle => _obstacle;

        public void Attacked(ZombieHealth zombie)
        {
            ZombieAttacked?.Invoke(zombie);
        }
    }
}