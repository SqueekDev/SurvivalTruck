using System;
using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private const int TimeBeforeSpawn = 1;

    [SerializeField] private WoodBlock _woodBlock;
    [SerializeField] private Boss _boss;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _zSpawnOffset;
    [SerializeField] private float _ySpawnOffset;

    private WaitForSeconds _delayBeforeSpawn = new WaitForSeconds(TimeBeforeSpawn);

    public Coroutine _spawnCorutine;

    public event Action<Boss> BossSpawned;

    private void OnEnable()
    {
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
        _levelChanger.BossLevelEnded += OnBossLevelEnded;
    }

    private void OnDisable()
    {
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        _levelChanger.BossLevelEnded -= OnBossLevelEnded;
    }

    private void OnBossLevelStarted()
    {
        if (_spawnCorutine != null)
        {
            StopCoroutine(_spawnCorutine);
        }

        _healthBar.gameObject.SetActive(true);
        _spawnCorutine = StartCoroutine(Spawn());
    }

    private void OnBossLevelEnded()
    {
        _healthBar.gameObject.SetActive(false);
    }

    private IEnumerator Spawn()
    {
        yield return _delayBeforeSpawn;
        Vector3 spawnPosition = new Vector3(_woodBlock.transform.position.x, _woodBlock.transform.position.y - _ySpawnOffset, _woodBlock.transform.position.z - _zSpawnOffset);
        _boss.transform.position = spawnPosition;
        _boss.gameObject.SetActive(true);
        BossSpawned?.Invoke(_boss);
    }
}
