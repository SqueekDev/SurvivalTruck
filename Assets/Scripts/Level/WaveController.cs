using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private BossSpawner _bossSpawner;
    [SerializeField] private int _startZombieinWaveCount;
    [SerializeField] private LevelWaveView _levelWaveView;
    [SerializeField] private AudioSource _backLevelSound;
    [SerializeField] private AudioSource _backBossSound;

    private int _attackingZombiesCount = 0;
    private int _ragedZombieCount = 0;
    private int _levelToAddedZombieMultiplier;
    private int _zombiesInWaveCount;
    private bool _isBossLevel;

    public event UnityAction WaveEnded;
    public event UnityAction<int, int> ZombieCountChanged;
    public event UnityAction<ZombieHealth> ZombieAttacked;

    private void Awake()
    {
        _levelToAddedZombieMultiplier = _levelChanger.BossLevelNumber;
    }

    private void OnEnable()
    {
        _levelChanger.Changed += OnlevelChanged;
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
        _bossSpawner.BossSpawned += OnBossSpawned;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked += OnZombieAttacked;
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnlevelChanged;
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        _bossSpawner.BossSpawned -= OnBossSpawned;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked -= OnZombieAttacked;
    }

    private void OnlevelChanged(int levelNumber)
    {
        if (_isBossLevel == false)
        {
            ChangeZombiesCount(levelNumber);
            _attackingZombiesCount = _zombiesInWaveCount;

            foreach (var rageArea in _rageAreas)
                rageArea.gameObject.SetActive(true);

            _levelWaveView.gameObject.SetActive(true);
            ZombieCountChanged?.Invoke(levelNumber, _zombiesInWaveCount);

            if (_backLevelSound.isPlaying==false)
            {
                if (_backBossSound.isPlaying)
                {
                    _backBossSound.Stop();
                }
                _backLevelSound.Play();
            }
        }
        else
        {
            if (_backBossSound.isPlaying == false)
            {
                if (_backLevelSound.isPlaying)
                {
                    _backLevelSound.Stop();
                }

                _backBossSound.Play();
            }
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
            foreach (var rageArea in _rageAreas)
                rageArea.gameObject.SetActive(false);
    }

    private void OnZombieDied(Health zombie)
    {
        zombie.Died -= OnZombieDied;
        _attackingZombiesCount--;
        if (_attackingZombiesCount <= 0)
        {
            _ragedZombieCount = 0;
            WaveEnded?.Invoke();
        }
    }

    private void ChangeZombiesCount(int levelNumber)
    {
        _zombiesInWaveCount = _startZombieinWaveCount + levelNumber / _levelToAddedZombieMultiplier;
    }
}
