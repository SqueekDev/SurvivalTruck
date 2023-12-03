using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Bullet _currentBulletTemplate;

    public float BulletSpeed => _bulletSpeed;
    public int Damage => _damage;
    public float TimeBetweenShoot => (StartDelay - _delayModifier/ Divider);

    private void Awake()
    {
        ChangeToStartBullet();
    }

    private void OnEnable()
    {
        _weaponDamageUpgradeButton.DamageUpgraded += OnDamageUpgraded;
        _weaponShootDelayUpgradeButton.ShootDelayUpgraded += OnShootDelayUpdated;
        _levelChanger.BossLevelStarted += ChangeToBossBullet;
        _levelChanger.BossLevelEnded += ChangeToStartBullet;
    }

    private void Start()
    {
        OnDamageUpgraded();
        OnShootDelayUpdated();
    }

    private void OnDisable()
    {
        _weaponDamageUpgradeButton.DamageUpgraded -= OnDamageUpgraded;        
        _weaponShootDelayUpgradeButton.ShootDelayUpgraded -= OnShootDelayUpdated;
        _levelChanger.BossLevelStarted -= ChangeToBossBullet;
        _levelChanger.BossLevelEnded -= ChangeToStartBullet;
    }

    public void Shoot(Transform target)
    {
        Bullet bullet = Instantiate(_currentBulletTemplate, _shootPoint.transform.position, Quaternion.identity);
        bullet.SetSpeed(_bulletSpeed);
        bullet.SetDamage(_damage);
        bullet.MoveTo(target.transform);
        _bulletSound.Play();
    }

    private void OnDamageUpgraded()
    {
        _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponDamage, _damage);
    }

    private void OnShootDelayUpdated()
    {
        _delayModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponShootDelay, _delayModifier);
    }

    private void ChangeToBossBullet()
    {
        _currentBulletTemplate = _bossBulletTemplate;
    }

    private void ChangeToStartBullet()
    {
        _currentBulletTemplate = _standartBulletTemplate;
    }
}
