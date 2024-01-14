using Base;
using Player;
using UnityEngine;

namespace Level
{
    public class CoinsSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPooler _coinsPooler;
        [SerializeField] private ObjectPooler _zombiePooler;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private int _maxCoinsCount = 5;
        [SerializeField] private AudioSource _coinsSound;

        private void OnEnable()
        {
            foreach (var zombie in _zombiePooler.PooledObjects)
            {
                zombie.GetComponent<Health>().Died += OnZombieDied;
            }
        }
        private void OnDisable()
        {
            foreach (var zombie in _zombiePooler.PooledObjects)
            {
                zombie.GetComponent<Health>().Died -= OnZombieDied;
            }
        }

        public void OnZombieDied(Health spawnPoint)
        {
            EnableRandomCoinsNumber(spawnPoint);
        }

        public void EnableRandomCoinsNumber(Health spawnPoint)
        {
            int randomCoinsCount = Random.Range(0, _maxCoinsCount);

            for (int i = 0; i < randomCoinsCount; i++)
            {
                if (_coinsPooler.TryGetObject(out GameObject pooledObject) && pooledObject.TryGetComponent(out Coin coin))
                {
                    coin.transform.position = spawnPoint.transform.position;
                    pooledObject.SetActive(true);
                    coin.MoveTarget(_player.transform);
                    _coinsSound.Play();
                }
            }
        }
    }
}