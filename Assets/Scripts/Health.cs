using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private int _additionalHealth;
    [SerializeField] private AudioSource _audioSource;

    private int _currentHealth;
    private Animator _animator;
    private Coroutine _dying;

    //protected HealthBar _healthBar;
    protected int AddHealthMultiplier = 0;

    public bool IsDead { get; private set; } = false;
    public int MaxHealth { get; protected set; }

    public event UnityAction<float> HealthChanged;
    public event UnityAction<Health> Died;

    protected virtual void OnEnable()
    {
        MaxHealth = _startHealth;
        ChangeMaxHealth();

        if (IsDead)
            IsDead = false;
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
        }
    }

    protected virtual void Die()
    {
        if (_dying == null)
        {
            IsDead = true;
            _dying = StartCoroutine(Dying());
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
            _currentHealth = MaxHealth;

        ChangeHealthStatus();
    }

    private void ChangeHealthStatus()
    {
        float currentHealthByMaxHealth = (float)_currentHealth / MaxHealth;
        HealthChanged?.Invoke(currentHealthByMaxHealth);
    }

    private IEnumerator Dying()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(2);
        _dying = null;
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
