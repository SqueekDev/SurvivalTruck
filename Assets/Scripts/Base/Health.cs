using System;
using System.Collections;
using UnityEngine;

namespace Base
{
    public class Health : MonoBehaviour
    {
        public const string DieTrigger = "Die";
        private const float DyingDelayTime = 2f;

        [SerializeField] private int _startHealth;
        [SerializeField] private int _additionalHealth;
        [SerializeField] private ColorChanger _colorChanger;

        protected int AddHealthMultiplier = 0;

        private int _currentHealth;
        private Animator _animator;
        private Coroutine _dying;

        private WaitForSeconds _dyingDelay = new WaitForSeconds(DyingDelayTime);

        public event Action<float> HealthChanged;

        public event Action<Health> Died;

        public bool IsDead { get; private set; } = false;

        public int MaxHealth { get; protected set; }

        protected virtual void OnEnable()
        {
            MaxHealth = _startHealth;
            ChangeMaxHealth();

            if (IsDead)
            {
                IsDead = false;
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(int count)
        {
            if (IsDead == false)
            {
                _currentHealth -= count;
                ChangeHealthStatus();

                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    Die();
                }
                else
                {
                    _colorChanger.ChangeToDamageColor();
                }
            }
        }

        protected virtual void ChangeMaxHealth()
        {
            MaxHealth = _startHealth + (_additionalHealth * AddHealthMultiplier);
            Heal(MaxHealth);
        }

        protected void Heal(int count)
        {
            _currentHealth += count;

            if (_currentHealth >= MaxHealth)
            {
                _currentHealth = MaxHealth;
            }

            ChangeHealthStatus();
        }

        private void ChangeHealthStatus()
        {
            float currentHealthByMaxHealth = (float)_currentHealth / MaxHealth;
            HealthChanged?.Invoke(currentHealthByMaxHealth);
        }

        private void Die()
        {
            if (_dying == null)
            {
                IsDead = true;
                _dying = StartCoroutine(Dying());
            }
        }

        private IEnumerator Dying()
        {
            Died?.Invoke(this);
            _animator.SetTrigger(DieTrigger);
            _colorChanger.ChangeColorDeath();
            yield return _dyingDelay;
            _dying = null;
            gameObject.SetActive(false);
            _colorChanger.ChangeColorNormal();
        }
    }
}