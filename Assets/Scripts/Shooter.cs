using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _stopShootingDistance;
    [SerializeField] private LayerMask _layerMask;

    private Coroutine _shooting;
    private bool _isShooting = false;
    private Zombie _currentTarget;
    private Player _selfHealth;

    public Zombie Target => _currentTarget;

    public bool IsShooting => _isShooting;

    private void Start()
    {
        _selfHealth = GetComponent<Player>();
    }

    private void Update()
    {
        if (_isShooting == false)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _shootingDistance, _layerMask);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Zombie zombie))
                {
                    if (zombie.IsDead == false && zombie.gameObject != gameObject && _shooting == null)
                    {
                        Shoot(zombie);
                        return;
                    }
                }
            }
        }

    }

    private void Shoot(Zombie target)
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

    private IEnumerator Shooting(Zombie zombie)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_weapon.TimeBetweenShoot);
        while (_currentTarget.IsDead == false && _currentTarget != null
             && Vector3.Distance(_currentTarget.transform.position, transform.position) < _stopShootingDistance)
        {
            _weapon.Shoot(_currentTarget.transform);
            yield return null;
            yield return waitForSeconds;
        }
        _isShooting = false;
        _shooting = null;
        _currentTarget = null;
    }
}
