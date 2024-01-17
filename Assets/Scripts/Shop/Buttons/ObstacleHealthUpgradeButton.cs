using Base;
using Truck;
using UnityEngine;

namespace Shop
{
    public class ObstacleHealthUpgradeButton : UpgradeButton
    {
        [SerializeField] private Obstacle _obstacle;

        private void Awake()
        {
            PlayerPrefsCurrentValue = PlayerPrefsKeys.ObstacleHealth;
            PlayerPrefsPrice = PlayerPrefsKeys.UpgradeObstacleHealthPrice;
        }

        protected override void OnEnable()
        {
            StartValue = _obstacle.MaxHealth;
            base.OnEnable();
        }
    }
}