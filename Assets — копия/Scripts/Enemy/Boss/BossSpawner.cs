using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private WoodBlock _woodBlock;
    [SerializeField] private Boss _template;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private float _zSpawnOffset;
    [SerializeField] private float _ySpawnOffset;
    [SerializeField] private int _delayBeforeSpawn;

    public Coroutine _spawnCorutine;

    public event UnityAction<Boss> BossSpawned;

    private void OnEnable()
    {
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
    }

    private void OnDisable()
    {
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
    }

    private void OnBossLevelStarted()
    {
        if (_spawnCorutine != null)
            StopCoroutine(_spawnCorutine);

        _spawnCorutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_delayBeforeSpawn);
        Vector3 spawnPosition = new Vector3(_woodBlock.transform.position.x, _woodBlock.transform.position.y - _ySpawnOffset, _woodBlock.transform.position.z - _zSpawnOffset);
        _template.transform.position = spawnPosition;
        _template.gameObject.SetActive(true);
        BossSpawned?.Invoke(_template);
    }
}
