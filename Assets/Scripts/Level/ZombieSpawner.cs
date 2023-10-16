using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float _delayBetweenSpawn;
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private ObjectPooler _zombiePooler;
    [SerializeField] private Car _car;
<<<<<<< Updated upstream
=======
    [SerializeField] private ZombieHealth _zombiePrefab;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y,_car.transform.position.z + _carZOffset);
            if (_zombiePooler.TryGetObject(out GameObject zombie))
=======
            Vector3 spawnPosition = new Vector3(randomX,_zombiePrefab. transform.position.y,_car.transform.position.z + _carZOffset);
            if (_zombiePooler.TryGetObject(out ZombieAttacker zombie))
>>>>>>> Stashed changes
            {
                zombie.transform.position = spawnPosition;
<<<<<<< Updated upstream
                zombie.SetActive(true);
=======
                zombie.transform.rotation = _zombiePrefab.transform.rotation;
                zombie.gameObject.SetActive(true);
>>>>>>> Stashed changes
            }

            yield return null;
            yield return delay;
        }
    }
}
