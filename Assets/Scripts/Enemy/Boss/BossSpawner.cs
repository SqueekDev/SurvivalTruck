using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private WoodBlock _woodBlock;
    [SerializeField] private Boss _boss;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private BossHealthBar _healthBar;
    [SerializeField] private float _zSpawnOffset;
    [SerializeField] private float _ySpawnOffset;
    [SerializeField] private int _delayBeforeSpawn;

    public Coroutine _spawnCorutine;

    public event UnityAction<Boss> BossSpawned;

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
            StopCoroutine(_spawnCorutine);

        _healthBar.gameObject.SetActive(true);
        _spawnCorutine = StartCoroutine(Spawn());
    }

    private void OnBossLevelEnded()
    {
        _healthBar.gameObject.SetActive(false);
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_delayBeforeSpawn);
        Vector3 spawnPosition = new Vector3(_woodBlock.transform.position.x, _woodBlock.transform.position.y - _ySpawnOffset, _woodBlock.transform.position.z - _zSpawnOffset);
        _boss.transform.position = spawnPosition;
        _boss.gameObject.SetActive(true);
        BossSpawned?.Invoke(_boss);
    }
}
