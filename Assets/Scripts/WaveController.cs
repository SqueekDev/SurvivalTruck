using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private List<RageArea> _rageAreas;
    [SerializeField] private int _startZombieinWaveCount;

    private int _attackingZombiesCount = 0;
    private int _levelToAddedZombieMultiplier = 5;
    private int _zombiesInWaveCount;

    public event UnityAction WaveEnded;

    private void OnEnable()
    {
        _levelChanger.Changed += OnlevelChanged;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked += OnZombieAttacked;
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnlevelChanged;

        foreach (var rageArea in _rageAreas)
            rageArea.ZombieAttacked -= OnZombieAttacked;
    }

    private void Start()
    {
        ChangeZombiesCount();
    }

    private void OnlevelChanged()
    {
        ChangeZombiesCount();

        foreach (var rageArea in _rageAreas)
            rageArea.gameObject.SetActive(true);
    }

    private void OnZombieAttacked(Zombie zombie)
    {
        zombie.Died += OnZombieDied;
        _attackingZombiesCount++;

        if (_attackingZombiesCount >= _zombiesInWaveCount)
            foreach (var rageArea in _rageAreas)
                rageArea.gameObject.SetActive(false);
    }

    private void OnZombieDied(Zombie zombie)
    {
        zombie.Died -= OnZombieDied;
        _attackingZombiesCount--;

        if (_attackingZombiesCount <= 0)
            WaveEnded?.Invoke();

    }

    private void ChangeZombiesCount()
    {
        _zombiesInWaveCount = _startZombieinWaveCount + _levelChanger.LevelNumber / _levelToAddedZombieMultiplier;
    }
}
