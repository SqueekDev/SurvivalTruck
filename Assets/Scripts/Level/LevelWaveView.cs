using Base;
using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelWaveView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Wave _waveController;
        [SerializeField] private Animation _waveViewAnimation;

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
            _slider.value = GlobalValues.Zero;
        }

        private void OnZombieAttacked(ZombieHealth zombie)
        {
            zombie.Died += OnZombieDied;
        }

        private void OnZombieDied(Health zombie)
        {
            _slider.value++;
            zombie.Died -= OnZombieDied;
            _waveViewAnimation.Play();

            if (_slider.value == _slider.maxValue)
            {
                gameObject.SetActive(false);
            }
        }
    }
}