using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const float StartDelay = 1;
    private const float Divider = 10;

    [SerializeField] WeaponDamageUpgradeButton _weaponDamageUpgradeButton;
    [SerializeField] WeaponShootDelayUpgradeButton _weaponShootDelayUpgradeButton;
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] private WeaponShootPoint _shootPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _delayModifier;
    //[SerializeField] protected ParticleSystem _particle;
    //[SerializeField] protected AudioSource _audioSource;
    //[SerializeField] protected string _savingName;

    public float BulletSpeed => _bulletSpeed;
    public int Damage => _damage;
    public float TimeBetweenShoot => (StartDelay - _delayModifier/ Divider);

    private void OnEnable()
    {
        _weaponDamageUpgradeButton.DamageUpgraded += OnDamageUpgraded;
        _weaponShootDelayUpgradeButton.ShootDelayUpgraded += OnShootDelayUpdated;
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
    }

    public void Shoot(Transform target)
    {
        //_particle.Play();
        //_audioSource.Play();
        Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, Quaternion.identity);
        bullet.SetSpeed(_bulletSpeed);
        bullet.SetDamage(_damage);
        bullet.MoveTo(target.transform);
    }

    private void OnDamageUpgraded()
    {
        _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponDamage, _damage);
    }

    private void OnShootDelayUpdated()
    {
        _delayModifier = PlayerPrefs.GetInt(PlayerPrefsKeys.WeaponShootDelay, _delayModifier);
    }
}
