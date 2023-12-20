using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public const string DieTrigger = "Die";
    private const float DyingDelayTime = 2f;
    private const float ChangeColorDelayTime = 0.2f;

    [SerializeField] private int _startHealth;
    [SerializeField] private int _additionalHealth;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _deathColor;
    [SerializeField] private Renderer _renderer;
    
    protected int AddHealthMultiplier = 0;

    private int _currentHealth;
    private Animator _animator;
    private Coroutine _dying;
    private Coroutine _changingColor;
    private Color _startColor;
    private WaitForSeconds _dyingDelay = new WaitForSeconds(DyingDelayTime);
    private WaitForSeconds _changeColorDelay = new WaitForSeconds(ChangeColorDelayTime);

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
        _startColor = _renderer.material.color;
    }

    public void TakeDamage(int count)
    {
        if (IsDead == false)
        {
            _currentHealth -= count;
            ChangeHealthStatus();

            if (_currentHealth <= GlobalValues.Zero)
            {
                _currentHealth = GlobalValues.Zero;
                Die();
            }
            else
            {
                if (_changingColor == null)
                {
                    _changingColor = StartCoroutine(ChangingColorDamage());
                }
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

    private IEnumerator Dying()
    {
        _animator.SetTrigger(DieTrigger);
        ChangeColorDeath();
        Died?.Invoke(this);
        yield return _dyingDelay;
        _dying = null;
        gameObject.SetActive(false);
        ChangeColorNormal();
    }

    private IEnumerator ChangingColorDamage()
    {
        _renderer.sharedMaterial.color = _damageColor;
        yield return _changeColorDelay;
        _renderer.sharedMaterial.color = _startColor;
        _changingColor = null;
    }

    private void ChangeColorDeath()
    {
        if (_changingColor != null)
        {
            StopCoroutine(_changingColor);
        }

        _renderer.sharedMaterial.color = _deathColor;
    }

    private void ChangeColorNormal()
    {
        _renderer.sharedMaterial.color = _startColor;
    }
}
