using Level;
using UnityEngine;

namespace UI
{
    public class PlayerHealthBarSwitcher : MonoBehaviour
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private HealthBar _bossLevelHealthBar;
        [SerializeField] private PlayerHealthBar _normalLevelHealthBar;

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

        private void OnBossLevelStarted()
        {
            _normalLevelHealthBar.gameObject.SetActive(false);
            _bossLevelHealthBar.gameObject.SetActive(true);
        }

        private void OnBossLevelEnded()
        {
            _bossLevelHealthBar.gameObject.SetActive(false);
            _normalLevelHealthBar.gameObject.SetActive(true);
        }
    }
}