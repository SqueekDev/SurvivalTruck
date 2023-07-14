using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private int _count;
    [SerializeField] private int _spawnSpread;
    [SerializeField] private List<Zombie> _templates;

    private Coroutine _spawnCorutine;

    private void Start()
    {
        if (_spawnCorutine != null)
            StopCoroutine(_spawnCorutine);
        else
            _spawnCorutine = StartCoroutine(InstantiateEnemies());
    }

    private IEnumerator InstantiateEnemies()
    {
        WaitForSeconds delay = new WaitForSeconds(_delayBetweenSpawn);

        for (int i = 0; i < _count; i++)
        {
            int spread = Random.Range(-_spawnSpread, _spawnSpread);
            Vector3 spawnPosition = new Vector3(transform.position.x + spread, transform.position.y, transform.position.z + spread);
            Zombie template = _templates[Random.Range(0, _templates.Count)];
            Zombie zombie = Instantiate(template, spawnPosition, Quaternion.identity);

            yield return delay;
        }
    }
}
