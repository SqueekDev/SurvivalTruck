using System.Collections;
using Enemy;
using Truck;
using UI;
using UnityEngine;

namespace Level
{
    public class BossSpawner : MonoBehaviour
    {
        private const int TimeBeforeSpawn = 1;

        [SerializeField] private WoodBlock _woodBlock;
        [SerializeField] private Boss _boss;
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private float _zSpawnOffset;
        [SerializeField] private float _ySpawnOffset;

        private Coroutine _spawnCorutine;
        private WaitForSeconds _delayBeforeSpawn = new WaitForSeconds(TimeBeforeSpawn);

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

        private IEnumerator EnablingBoss()
        {
            yield return _delayBeforeSpawn;
            Vector3 spawnPosition = new Vector3(
                _woodBlock.transform.position.x,
                _woodBlock.transform.position.y - _ySpawnOffset,
                _woodBlock.transform.position.z - _zSpawnOffset);
            _boss.transform.position = spawnPosition;
            _boss.gameObject.SetActive(true);
        }

        private void OnBossLevelStarted()
        {
            if (_spawnCorutine != null)
            {
                StopCoroutine(_spawnCorutine);
            }

            _healthBar.gameObject.SetActive(true);
            _spawnCorutine = StartCoroutine(EnablingBoss());
        }

        private void OnBossLevelEnded()
        {
            _healthBar.gameObject.SetActive(false);
        }
    }
}