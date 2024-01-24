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

        private float _minSpread = -1f;
        private float _maxSpred = 1f;

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

        private int GetRandomCoinsNumber()
        {
            int coinsCount = Random.Range(0, _maxCoinsCount);
            return coinsCount;
        }

        private Vector3 GetSpawnPoint(Health zombie)
        {
            float spread = Random.Range(_minSpread, _maxSpred);
            Vector3 spawnPoint = new Vector3(
                zombie.transform.position.x + spread, 
                zombie.transform.position.y, 
                zombie.transform.position.z + spread);
            return spawnPoint;
        }

        private void EnableCoins(Health zombie)
        {
            int coinsCount = GetRandomCoinsNumber();

            for (int i = 0; i < coinsCount; i++)
            {
                if (_coinsPooler.TryGetObject(out Coin coin))
                {
                    coin.transform.position = GetSpawnPoint(zombie);
                    coin.gameObject.SetActive(true);
                    coin.MoveTarget(_player.transform);
                    _coinsSound.Play();
                }
            }
        }

        private void OnZombieDied(Health spawnPoint)
        {
            EnableCoins(spawnPoint);
        }
    }
}