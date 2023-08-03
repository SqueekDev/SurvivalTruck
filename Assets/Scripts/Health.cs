using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected string _savingName;
    [SerializeField] protected AudioSource _audioSource;

    protected bool _isDead = false;

    //protected HealthBar _healthBar;
    protected int _currentHealth;
    //protected AnimatorController _animatorController;

    public bool IsDead => _isDead;

    public event UnityAction<float> HealthChanged;

    private void OnEnable()
    {
        Heal(_maxHealth);
        if (_isDead)
        {
            _isDead = false;
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(_savingName))
        {
            _maxHealth = PlayerPrefs.GetInt(_savingName);
        }
        _currentHealth = _maxHealth;
        //_animatorController = GetComponent<AnimatorController>();
    }
    public void TakeDamage(int count)
    {
        _currentHealth -= count;
        float currentHealthByMaxHealth = (float)_currentHealth / _maxHealth;
        HealthChanged?.Invoke(currentHealthByMaxHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
            _isDead = true;
        }
    }
    public void Heal(int count)
    {
        _currentHealth += count;
        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        float currentHealthByMaxHealth = (float)_currentHealth / _maxHealth;
        HealthChanged?.Invoke(currentHealthByMaxHealth);
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }

}
