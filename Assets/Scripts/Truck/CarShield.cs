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
        [SerializeField] private CarShieldDamageUpgradeButton _kangarooDamageUpgradeButton;
        [SerializeField] private ParticleSystem _poofKangarooPartical;

        private WaitForSeconds _delay = new WaitForSeconds(ApplyingDamageDelay);

        public event Action<ZombieHealth> ZombieHited;

        public int Damage => _damage;

        private void Awake()
        {
            OnDamageUpgraded();
        }

        private void OnEnable()
        {
            _kangarooDamageUpgradeButton.SkillUpgraded += OnDamageUpgraded;
        }

        private void OnDisable()
        {
            _kangarooDamageUpgradeButton.SkillUpgraded -= OnDamageUpgraded;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ZombieHealth zombie))
            {
                ZombieHited?.Invoke(zombie);
                StartCoroutine(ApplyingDamage(zombie));
                _poofKangarooPartical.transform.position = zombie.transform.position;
                _poofKangarooPartical.Play();
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