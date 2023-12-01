using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelWaveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private Slider _slider;
    [SerializeField] private WaveController _waveController;

    private void OnEnable()
    {
        _waveController.ZombieCountChanged += OnZombieCountChanged;
        _waveController.ZombieAttacked += OnZombieAttacked;
    }

    private void OnDisable()
    {
        _waveController.ZombieCountChanged -= OnZombieCountChanged;
        _waveController.ZombieAttacked -= OnZombieAttacked;
    }

    private void OnZombieCountChanged(int levelNumber, int zombiesInWave)
    {
        _levelNumberText.text = levelNumber.ToString();
        _slider.maxValue = zombiesInWave;
        _slider.value = 0;
    }

    private void OnZombieAttacked(ZombieHealth zombie)
    {
        zombie.Died += OnZombieDied;
    }

    private void OnZombieDied(Health zombie)
    {
        _slider.value++;
        zombie.Died -= OnZombieDied;

        if (_slider.value==_slider.maxValue)
            gameObject.SetActive(false);
    }
}
