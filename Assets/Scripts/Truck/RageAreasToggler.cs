using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Truck
{
    public class RageAreasToggler : MonoBehaviour
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private Wave _wave;
        [SerializeField] private List<RageArea> _rageAreas;

        private void OnEnable()
        {
            _levelChanger.NormalLevelStarted += OnNormalLevelStarted;
            _wave.AllZombiesAttacked += OnAllZombiesAttacked;
        }

        private void OnDisable()
        {
            _levelChanger.NormalLevelStarted -= OnNormalLevelStarted;
            _wave.AllZombiesAttacked -= OnAllZombiesAttacked;
        }

        private void Toggle(bool enabled)
        {
            foreach (var rageArea in _rageAreas)
            {
                rageArea.gameObject.SetActive(enabled);
            }
        }

        private void OnNormalLevelStarted()
        {
            Toggle(true);
        }

        private void OnAllZombiesAttacked()
        {
            Toggle(false);
        }
    }
}