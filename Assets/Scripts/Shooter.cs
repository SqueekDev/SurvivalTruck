using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _timeBetweenShoot;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _stopShootingDistance;
    [SerializeField] private LayerMask _layerMask;

    private Coroutine _shooting;
    private bool _isShooting = false;
    private Transform _currentTarget;
    private Player _selfHealth;

    public Transform Target => _currentTarget;
    public int Damage => _damage;
    public float BulletSpeed => _bulletSpeed;

    public bool IsShooting => _isShooting;

    private void Start()
    {
        _selfHealth = GetComponent<Player>();
    }

    private void Update()
    {
        if (_isShooting==false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _shootingDistance, _layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Health health))
                {
                    if (health.IsDead == false && health.gameObject != gameObject && _shooting == null)
                    {
                        Shoot(health.transform);
                        return;
                    }
                }
            }
        }

    }

    private void Shoot(Transform target)
    {
        _currentTarget = target;
        _isShooting = true;
        _shooting = StartCoroutine(Shooting(_currentTarget));

    }

    private void StopShoot()
    {
        if (_shooting != null)
        {
            StopCoroutine(_shooting);
            _shooting = null;
            _currentTarget = null;
            _isShooting = false;
        }

    }

    private IEnumerator Shooting(Transform target)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_timeBetweenShoot);
        while (_currentTarget.gameObject.activeSelf&&_currentTarget != null
             && Vector3.Distance(_currentTarget.transform.position, transform.position) < _stopShootingDistance)
        {
            _weapon.Shoot(_currentTarget);
            yield return null;
            yield return waitForSeconds;
        }
        _isShooting = false;
        _shooting = null;
        _currentTarget = null;
    }
}
