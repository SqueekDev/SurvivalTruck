using UnityEngine;
using Level;

namespace Sounds
{
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        private const float NormalLevelVolume = 0.05f;
        private const float BossLevelVolume = 0.1f;

        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _normalLevelMusic;
        [SerializeField] private AudioClip _bossLevelMusic;

        private void Awake()
        {
            ChangeMusic(_normalLevelMusic, NormalLevelVolume);
        }

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

        private void ChangeMusic(AudioClip music, float volume)
        {
            _audioSource.clip = music;
            _audioSource.volume = volume;
        }

        private void OnBossLevelStarted()
        {
            _audioSource.Stop();
            ChangeMusic(_bossLevelMusic, BossLevelVolume);
            _audioSource.Play();
        }

        private void OnBossLevelEnded()
        {
            _audioSource.Stop();
            ChangeMusic(_normalLevelMusic, NormalLevelVolume);
            _audioSource.Play();
        }
    }
}