using System.Collections;
using Base;
using Enemy;
using UnityEngine;

namespace Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _shootingDistance;
        [SerializeField] private float _stopShootingDistance;
        [SerializeField] private LayerMask _layerMask;

        private Coroutine _shooting;
        private ZombieHealth _currentTarget;
        private bool _isShooting = false;

        public ZombieHealth Target => _currentTarget;

        public bool IsShooting => _isShooting;

        private void Update()
        {
            if (_isShooting == false)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _shootingDistance, _layerMask);

                if (colliders.Length > GlobalValues.Zero)
                {
                    foreach (var collider in colliders)
                    {
                        if (collider.gameObject.TryGetComponent(out ZombieHealth zombie) &&
                            zombie.IsAngry &&
                            zombie.IsDead == false &&
                            zombie.gameObject != gameObject &&
                            _shooting == null)
                        {
                            Shoot(zombie);
                            return;
                        }
                    }
                }
            }
        }

        private void Shoot(ZombieHealth target)
        {
            _currentTarget = target;
            _isShooting = true;
            _shooting = StartCoroutine(Shooting(_currentTarget));
        }

        private IEnumerator Shooting(ZombieHealth zombie)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_weapon.TimeBetweenShoot);

            while (zombie.IsDead == false &&
                _currentTarget != null &&
                Vector3.Distance(_currentTarget.transform.position, transform.position) < _stopShootingDistance)
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
}