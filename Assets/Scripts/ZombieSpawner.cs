using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private ObjectPooler _zombiePooler;
    [SerializeField] private Car _car;
    [SerializeField] private float _carZOffset;


    private void Start()
    {      
       StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        WaitForSeconds delay = new WaitForSeconds(_delayBetweenSpawn);

        while (true)
        {
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y,_car.transform.position.z + _carZOffset);
            if (_zombiePooler.TryGetObject(out GameObject zombie))
            {
                zombie.transform.position = spawnPosition;
                zombie.SetActive(true);
            }

            yield return null;
            yield return delay;
        }
    }
}
