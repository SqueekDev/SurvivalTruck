using Base;
using Level;
using Shop;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        private const float StartDelay = 1;
        private const float Divider = 10;

        [SerializeField] private WeaponDamageUpgradeButton _weaponDamageUpgradeButton;
        [SerializeField] private WeaponShootDelayUpgradeButton _weaponShootDelayUpgradeButton;
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private Bullet _standartBulletTemplate;
        [SerializeField] private BossBullet _bossBulletTemplate;
        [SerializeField] private WeaponShootPoint _shootPoint;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private int _delayModifier;
        [SerializeField] private AudioSource _bulletSound;
        [SerializeField] private ParticleSystem _shootEffect;

        private Bullet _currentBulletTemplate;

        public int Damage => _damage;

        public float TimeBetweenShoot => StartDelay - _delayModifier / Divider;

        private void Awake()
        {
            OnBossLevelEnded();
        }

        private void OnEnable()
        {
            _weaponDamageUpgradeButton.SkillUpgraded += OnDamageUpgraded;
            _weaponShootDelayUpgradeButton.SkillUpgraded += OnShootDelayUpdated;
            _levelChanger.BossLevelStarted += OnBossLevelStarted;
            _levelChanger.BossLevelEnded += OnBossLevelEnded;
        }

        private void Start()
        {
            OnDamageUpgraded();
            OnShootDelayUpdated();
        }

        private void OnDisable()
        {
            _weaponDamageUpgradeButton.SkillUpgraded -= OnDamageUpgraded;
            _weaponShootDelayUpgradeButton.SkillUpgraded -= OnShootDelayUpdated;
            _levelChanger.BossLevelStarted -= OnBossLevelStarted;
            _levelChanger.BossLevelEnded -= OnBossLevelEnded;
        }

        public void Shoot(Transform target)
        {
            Bullet bullet = Instantiate(_currentBulletTemplate, _shootPoint.transform.position, Quaternion.identity);
            bullet.SetSpeed(_bulletSpeed);
            bullet.SetDamage(_damage);
            bullet.MoveTo(target.transform);
            _bulletSound.Play();
            _shootEffect.Play();
        }

        private void OnDamageUpgraded()
        {
            _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponDamage, _damage);
        }

        private void OnShootDelayUpdated()
        {
            _delayModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponShootDelay, _delayModifier);
        }

        private void OnBossLevelStarted()
        {
            _currentBulletTemplate = _bossBulletTemplate;
        }

        private void OnBossLevelEnded()
        {
            _currentBulletTemplate = _standartBulletTemplate;
        }
    }
}