using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private const int LevelToAddedZombieMultiplier = 3;
    private const int MaxZombiesInWaveCount = 14;

    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private BossSpawner _bossSpawner;
    [SerializeField] private int _startZombieinWaveCount;
    [SerializeField] private LevelWaveView _levelWaveView;

    private int _attackingZombiesCount = 0;
    private int _ragedZombieCount = 0;
    private int _zombiesInWaveCount;
    private bool _isBossLevel;
    private bool _isMaxZombieCount = false;

    public event Action WaveEnded;
    public event Action<int, int> ZombieCountChanged;
    public event Action<ZombieHealth> ZombieAttacked;

    private void OnEnable()
    {
        _levelChanger.Changed += OnlevelChanged;
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
        _bossSpawner.BossSpawned += OnBossSpawned;

        foreach (var rageArea in _rageAreas)
        {
            rageArea.ZombieAttacked += OnZombieAttacked;
        }
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnlevelChanged;
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        _bossSpawner.BossSpawned -= OnBossSpawned;

        foreach (var rageArea in _rageAreas)
        {
            rageArea.ZombieAttacked -= OnZombieAttacked;
        }
    }

    private void ChangeZombiesCount(int levelNumber)
    {
        _zombiesInWaveCount = _startZombieinWaveCount + levelNumber / LevelToAddedZombieMultiplier;

        if (_zombiesInWaveCount >= MaxZombiesInWaveCount)
        {
            _zombiesInWaveCount = MaxZombiesInWaveCount;
            _isMaxZombieCount = true;
        }
    }

    private void OnlevelChanged(int levelNumber)
    {
        if (_isBossLevel == false)
        {
            if (_isMaxZombieCount == false)
            {
                ChangeZombiesCount(levelNumber);
            }

            _attackingZombiesCount = _zombiesInWaveCount;

            foreach (var rageArea in _rageAreas)
            {
                rageArea.gameObject.SetActive(true);
            }

            _levelWaveView.gameObject.SetActive(true);
            ZombieCountChanged?.Invoke(levelNumber, _zombiesInWaveCount);
        }
    }

    private void OnBossLevelStarted()
    {
        _isBossLevel = true;
    }

    private void OnBossSpawned(Health boss)
    {
        boss.Died += OnBossDied;
    }

    private void OnBossDied(Health boss)
    {
        _isBossLevel = false;
        boss.Died -= OnBossDied;
        WaveEnded?.Invoke();
    }

    private void OnZombieAttacked(ZombieHealth zombie)
    {
        zombie.Died += OnZombieDied;
        _ragedZombieCount++;
        ZombieAttacked?.Invoke(zombie);

        if (_ragedZombieCount >= _zombiesInWaveCount)
        {
            foreach (var rageArea in _rageAreas)
            {
                rageArea.gameObject.SetActive(false);
            }
        }
    }

    private void OnZombieDied(Health zombie)
    {
        zombie.Died -= OnZombieDied;
        _attackingZombiesCount--;

        if (_attackingZombiesCount <= GlobalValues.Zero)
        {
            _ragedZombieCount = GlobalValues.Zero;
            WaveEnded?.Invoke();
        }
    }
}
