using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] private WeaponShootPoint _shootPoint;
    //[SerializeField] protected ParticleSystem _particle;
    //[SerializeField] protected AudioSource _audioSource;
    //[SerializeField] protected string _savingName;

    protected Shooter _shooter;
    protected float _bulletSpeed;
    protected int _damage;

    private void Start()
    {
        _shooter = GetComponentInParent<Shooter>();
        _bulletSpeed = _shooter.BulletSpeed;
        _damage = _shooter.Damage;
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
}
