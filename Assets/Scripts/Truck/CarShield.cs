using System;
using System.Collections;
using Base;
using Enemy;
using Shop;
using UnityEngine;

namespace Truck
{
    public class CarShield : MonoBehaviour
    {
        private const float ApplyingDamageDelay = 0.5f;

        [SerializeField] private int _damage;
        [SerializeField] private CarShieldDamageUpgradeButton _CarShieldDamageUpgradeButton;
        [SerializeField] private ParticleSystem _poofCarShieldPartical;

        private WaitForSeconds _delay = new WaitForSeconds(ApplyingDamageDelay);

        public event Action<ZombieHealth> ZombieHited;

        public int Damage => _damage;

        private void Awake()
        {
            OnDamageUpgraded();
        }

        private void OnEnable()
        {
            _CarShieldDamageUpgradeButton.SkillUpgraded += OnDamageUpgraded;
        }

        private void OnDisable()
        {
            _CarShieldDamageUpgradeButton.SkillUpgraded -= OnDamageUpgraded;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ZombieHealth zombie))
            {
                ZombieHited?.Invoke(zombie);
                StartCoroutine(ApplyingDamage(zombie));
                _poofCarShieldPartical.transform.position = zombie.transform.position;
                _poofCarShieldPartical.Play();
            }
        }

        private IEnumerator ApplyingDamage(Health health)
        {
            yield return _delay;
            health.TakeDamage(_damage);
        }

        private void OnDamageUpgraded()
        {
            _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.CarShieldDamage, _damage);
        }
    }
}