using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private string _savingName;
    [SerializeField]private Mover _mover;
    [SerializeField] private AudioSource _audioSource;

    protected bool _isDead = false;

    //protected HealthBar _healthBar;
    private int _currentHealth;
    private Animator _animator;
    private Coroutine _dying;

    public bool IsDead => _isDead;

    public event UnityAction<float> HealthChanged;
    public event UnityAction<Health> Died;

    private void OnEnable()
    {
        Heal(_maxHealth);
        if (_isDead)
        {
            _mover.SetStartSpeed();
            _isDead = false;
        }
    }
    private void Start()
    {
        /*if (PlayerPrefs.HasKey(_savingName))
        {
            _maxHealth = PlayerPrefs.GetInt(_savingName);
        }*/
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(int count)
    {
        if (_isDead==false)
        {

            _currentHealth -= count;
            float currentHealthByMaxHealth = (float)_currentHealth / _maxHealth;
            HealthChanged?.Invoke(currentHealthByMaxHealth);
            Debug.Log(_currentHealth);

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }

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
        if (_dying==null)
        {
            _mover.SetNoSpeed();
            _isDead = true;
            _dying = StartCoroutine(Dying());
        }

    }
    private IEnumerator Dying()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(3);
        Died?.Invoke(this);
        _dying = null;
        gameObject.SetActive(false);
    }
}
