using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Cabine _cabine;
    [SerializeField] private Boss _template;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private int _zSpawnOffset;
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
        Vector3 spawnPosition = new Vector3(_cabine.transform.position.x, _cabine.transform.position.y, _cabine.transform.position.z - _zSpawnOffset);
        _template.transform.position = spawnPosition;
        _template.gameObject.SetActive(true);
        BossSpawned?.Invoke(_template);
    }
}
