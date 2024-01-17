using System;
using Base;
using Level;
using Shop;
using UnityEngine;

namespace Truck
{
    public class Obstacle : MonoBehaviour
    {
        private const int HalfHPDivider = 2;

        [SerializeField] private int _maxHealth;
        [SerializeField] private RepairZone _repairZone;
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private Wave _waveController;
        [SerializeField] private ObstacleHealthUpgradeButton _obstacleHealthUpgradeButton;
        [SerializeField] private ParticleSystem _poofParticle;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _woodHitSound;
        [SerializeField] private AudioClip _destroyWoodSound;

        private int _currentHealth;
        private bool _upperBlockDestroyed;
        private bool _middleBlockDestroyed;
        private bool _lowerBlockDestroyed;

        public event Action UpperBlockDestroyed;

        public event Action MiddleBlockDestroyed;

        public event Action LowerBlockDestroyed;

        public event Action BlocksRepaired;

        public int MaxHealth => _maxHealth;

        public bool IsDestroyed { get; private set; }

        private void OnEnable()
        {
            _repairZone.Repaired += OnRepaired;
            _levelChanger.Changed += OnLevelChanged;
            _waveController.Ended += OnWaveEnded;
            _obstacleHealthUpgradeButton.SkillUpgraded += OnHealthUpgraded;
        }

        private void Start()
        {
            OnHealthUpgraded();
        }

        private void OnDisable()
        {
            _repairZone.Repaired -= OnRepaired;
            _levelChanger.Changed -= OnLevelChanged;
            _waveController.Ended -= OnWaveEnded;
            _obstacleHealthUpgradeButton.SkillUpgraded -= OnHealthUpgraded;
        }

        public void ApplyDamade(int damage)
        {
            _currentHealth -= damage;
            _audioSource.PlayOneShot(_woodHitSound);

            if (_currentHealth < _maxHealth && _upperBlockDestroyed == false)
            {
                PlayDestroyEffects();
                UpperBlockDestroyed?.Invoke();
                _upperBlockDestroyed = true;
            }

            if (_currentHealth < _maxHealth / HalfHPDivider && _middleBlockDestroyed == false)
            {
                PlayDestroyEffects();
                MiddleBlockDestroyed?.Invoke();
                _middleBlockDestroyed = true;
            }

            if (_currentHealth <= 0 && _lowerBlockDestroyed == false)
            {
                PlayDestroyEffects();
                LowerBlockDestroyed?.Invoke();
                _currentHealth = 0;
                _lowerBlockDestroyed = true;
                IsDestroyed = true;
            }
        }

        private void PlayDestroyEffects()
        {
            _poofParticle.Play();
            _audioSource.PlayOneShot(_destroyWoodSound);
        }

        private void OnLevelChanged(int level)
        {
            _repairZone.gameObject.SetActive(false);
        }

        private void OnWaveEnded()
        {
            if (_currentHealth < _maxHealth)
            {
                _repairZone.gameObject.SetActive(true);
            }
        }

        private void OnRepaired()
        {
            BlocksRepaired?.Invoke();
            _currentHealth = _maxHealth;
            IsDestroyed = false;
            _upperBlockDestroyed = false;
            _middleBlockDestroyed = false;
            _lowerBlockDestroyed = false;
            _repairZone.gameObject.SetActive(false);
        }

        private void OnHealthUpgraded()
        {
            _maxHealth = PlayerPrefs.GetInt(PlayerPrefsKeys.ObstacleHealth, _maxHealth);
            OnRepaired();
        }
    }
}