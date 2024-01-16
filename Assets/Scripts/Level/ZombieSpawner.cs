using System.Collections;
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
            StartCoroutine(EnableTemplates());
        }

        private IEnumerator EnableTemplates()
        {
            while (_car != null)
            {
                float randomX = Random.Range(_xLimits.x, _xLimits.y);

                if (_zombiePooler.TryGetObject(out GameObject zombie))
                {
                    float startY = 0.5f;
                    Vector3 spawnPosition = new Vector3(randomX, startY, _car.transform.position.z + _carZOffset);
                    zombie.transform.position = spawnPosition;
                    zombie.transform.SetParent(null);
                    zombie.transform.rotation = Quaternion.identity;
                    zombie.SetActive(true);
                }

                yield return null;
                yield return DelayBetweenSpawn;
            }
        }
    }
}