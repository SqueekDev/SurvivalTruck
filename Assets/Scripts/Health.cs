using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private int _additionalHealth;
    [SerializeField] private int _dyingDelay=2;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _deathColor;
    [SerializeField] private Renderer _renderer;

    private int _currentHealth;
    private Animator _animator;
    private Coroutine _dying;
    private Coroutine _changingColor;
    private Color _startColor;

    protected int AddHealthMultiplier = 0;

    public bool IsDead { get; private set; } = false;
    public int MaxHealth { get; protected set; }

    public const string DieTrigger = "Die";

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
        _startColor=_renderer.material.color;
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
                if (_changingColor==null)
                {
                    _changingColor = StartCoroutine(ChangingColorDamage());
                }
            }
        }
    }

    public virtual void Die()
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
        _animator.SetTrigger(DieTrigger);
        ChangeColorDeath();
        Died?.Invoke(this);
        yield return new WaitForSeconds(_dyingDelay);
        _dying = null;
        gameObject.SetActive(false);
        ChangeColorNormal();
    }

    private IEnumerator ChangingColorDamage()
    {
        _renderer.sharedMaterial.color= _damageColor;
        yield return new WaitForSeconds(0.2f);
        _renderer.sharedMaterial.color = _startColor;
        _changingColor = null;
    }
    
    private void ChangeColorDeath()
    {
        if (_changingColor!=null)
        {
            StopCoroutine(_changingColor);
        }
        _renderer.sharedMaterial.color= _deathColor;
    }
    
    private void ChangeColorNormal()
    {
        _renderer.sharedMaterial.color= _startColor;
    }
}
