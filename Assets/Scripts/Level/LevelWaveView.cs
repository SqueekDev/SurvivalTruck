using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelWaveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Slider _slider;
    [SerializeField] private ObjectPooler _zombiePooler;

    private void OnEnable()
    {
        foreach (var zombie in _zombiePooler.PooledObjects)
        {
            if (zombie.TryGetComponent(out ZombieHealth zombieHealth))
            {
                zombieHealth.Died += OnZombieDied;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var zombie in _zombiePooler.PooledObjects)
        {
            if (zombie.TryGetComponent(out ZombieHealth zombieHealth))
            {
                zombieHealth.Died -= OnZombieDied;
            }
        }
    }

    public void SetLevelNumber(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
        _slider.maxValue = levelNumber;
        _slider.value = 0;
    }

    private void OnZombieDied(Health zombie)
    {
        _slider.value++;
        if (_slider.value==_slider.maxValue)
        {
            gameObject.SetActive(false);
        }
    }
}
