using Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Slider _hpSlider;


        private void OnEnable()
        {
            _hpSlider.value = 0;
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float precentageValue)
        {
            _hpSlider.value = precentageValue;
        }
    }
}