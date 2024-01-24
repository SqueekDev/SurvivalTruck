using System.Collections;
using Base;
using Enemy;
using Truck;
using UnityEngine;

namespace Level
{
    public class ZombieSpawner : MonoBehaviour
    {
        private const float TimeBetweenSpawn = 1f;

        [SerializeField] private Vector2 _xLimits;
        [SerializeField] private ObjectPooler _zombiePooler;
        [SerializeField] private Car _car;
        [SerializeField] private float _carZOffset;

        private WaitForSeconds DelayBetweenSpawn = new WaitForSeconds(TimeBetweenSpawn);

        private void Start()
        {
            StartCoroutine(EnablingEnemies());
        }

        private Zombie TryGetEnemy()
        {
            if (_zombiePooler.TryGetObject(out Zombie zombie))
            {
                return zombie;
            }
            else
            {
                return null;
            }
        }

        private Vector3 GetSpawnPosition()
        {
            float randomX = Random.Range(_xLimits.x, _xLimits.y);
            float startY = 0.5f;
            Vector3 spawnPosition = new Vector3(randomX, startY, _car.transform.position.z + _carZOffset);
            return spawnPosition;
        }

        private IEnumerator EnablingEnemies()
        {
            while (true)
            {
                Zombie zombie = TryGetEnemy();

                if (zombie != null)
                {
                    zombie.transform.position = GetSpawnPosition();
                    zombie.transform.SetParent(null);
                    zombie.transform.rotation = Quaternion.identity;
                    zombie.gameObject.SetActive(true);
                }

                yield return null;
                yield return DelayBetweenSpawn;
            }
        }
    }
}