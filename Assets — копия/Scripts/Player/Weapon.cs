using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] private WeaponShootPoint _shootPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeBetweenShoot;
    //[SerializeField] protected ParticleSystem _particle;
    //[SerializeField] protected AudioSource _audioSource;
    //[SerializeField] protected string _savingName;

    public float BulletSpeed => _bulletSpeed;
    public int Damage => _damage;
    public float TimeBetweenShoot => _timeBetweenShoot;

    public void Shoot(Transform target)
    {
        //_particle.Play();
        //_audioSource.Play();
        Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.transform.position, Quaternion.identity);
        bullet.SetSpeed(_bulletSpeed);
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.PlayerDamage))
        {
            _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerDamage);
        }
        bullet.SetDamage(_damage);
        bullet.MoveTo(target.transform);
    }
}
