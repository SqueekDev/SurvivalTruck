using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
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