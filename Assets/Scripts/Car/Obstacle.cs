using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    private const int Doubler = 2;

    [SerializeField] private int _maxHealth;
    [SerializeField] private List<WoodBlock> _blocks;
    [SerializeField] private RepairZone _repairZone;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private ObstacleHealthUpgradeButton _obstacleHealthUpgradeButton;
    [SerializeField] private ParticleSystem _poofParticle;

    private int _currentHealth;
    private bool _upperBlockDestroyed;
    private bool _middleBlockDestroyed;
    private bool _lowerBlockDestroyed;

    public int MaxHealth => _maxHealth;

    public bool IsDestroyed { get; private set; }

    public event UnityAction UpperBlockDestroyed;
    public event UnityAction MiddleBlockDestroyed;
    public event UnityAction LowerBlockDestroyed;
    public event UnityAction BlocksRepaired;

    private void OnEnable()
    {
        _repairZone.Repaired += OnRepaired;
        _levelChanger.Changed += OnLevelChanged;
        _waveController.WaveEnded += OnWaveEnded;
        _obstacleHealthUpgradeButton.HealthUpgraded += OnHealthUpgraded;
    }

    private void Start()
    {
        OnHealthUpgraded();
    }

    private void OnDisable()
    {
        _repairZone.Repaired -= OnRepaired;
        _levelChanger.Changed -= OnLevelChanged;
        _waveController.WaveEnded -= OnWaveEnded;
        _obstacleHealthUpgradeButton.HealthUpgraded -= OnHealthUpgraded;
    }

    public void ApplyDamade(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < _maxHealth / _blocks.Count * Doubler && _upperBlockDestroyed == false)
        {
            _poofParticle.Play();
            UpperBlockDestroyed?.Invoke();
            _upperBlockDestroyed = true;
        }

        if (_currentHealth < _maxHealth / _blocks.Count && _middleBlockDestroyed == false)
        {
            _poofParticle.Play();
            MiddleBlockDestroyed?.Invoke();
            _middleBlockDestroyed = true;
        }    

        if (_currentHealth <= 0 && _lowerBlockDestroyed == false)
        {
            _poofParticle.Play();
            LowerBlockDestroyed?.Invoke();
            _currentHealth = 0;
            _lowerBlockDestroyed = true;
            IsDestroyed = true;
        }
    }

    private void OnLevelChanged(int level)
    {
        _repairZone.gameObject.SetActive(false);
    }

    private void OnWaveEnded()
    {
        _repairZone.gameObject.SetActive(true);
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
